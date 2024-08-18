using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Base;
using Data.Entities;
using Microsoft.IdentityModel.Tokens;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Azure;
using Azure.Core;
using Core.Models.Entities.System;
using Core.Services;
using System.IO;
using Core.Models.File;
using System.Buffers.Text;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Domain.RequestModel;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Services.Admin.Horisontal
{
    public class Horisontal : IHorisontal
    {
        private readonly IAppData<Data.Entities.Horisontal> _horisontal;
        private readonly IAppData<Data.Entities.Image> _image;
        private readonly IFileManager _fileManager;
        private readonly IAppData<BaseFile> _file;
        private readonly IAppData<DataType> _dataType;

        public Horisontal(
               IAppData<Data.Entities.Horisontal> horisontal,
               IAppData<Data.Entities.Image> image,
               IFileManager fileManager,
               IAppData<BaseFile> file,
               IAppData<DataType> dataType
        )
        {
            _horisontal = horisontal;
            _image = image;
            _fileManager = fileManager;
            _file = file;
            _dataType = dataType;
        }

        public async Task<HorisontalViewModel> GetHorisontal(long? horisontalId)
        {
            if (horisontalId == null)
                throw new ApiException("Не указан идентификатор для horisontal");

            var horisontalGuid = horisontalId.Value.ToString();

            var listImages = _horisontal.TableNoTracking.Where(h => h.Id == horisontalId && !h.IsDeleted).Select(h => h.About!.Images).FirstOrDefault()!.ToList();

            var resultImage = listImages.Where(i => _fileManager.GetFileLinksByName(4, horisontalGuid, i.Main!).FirstOrDefault() != null).Select(i => new ImageLink
            {
                Link = _fileManager.GetFileLinksByName(4, horisontalGuid, i.Main!).FirstOrDefault(),
                Name = i.Main,
                Priority = i.Priority,
                Id = i.Id
            }).OrderBy(i => i.Priority).ToList();

            var responce = _horisontal.TableNoTracking.Where(h => h.Id == horisontalId)
                .Include(h => h.Img)
                .Include(h => h.About)
                .Include(h => h.About!.Background)
                .Include(h => h.About!.Title)
                .Include(h => h.About!.Main)
                .Include(h => h.About!.Main!.Img)
                .Include(h => h.About!.Title!.Img)
                .ToList()
                .Select(h => new HorisontalViewModel()
            {
                Id = h.Id,
                Img = _fileManager.GetFileLinksByName(1, horisontalGuid, h.Img!.Main!).FirstOrDefault(),
                X = h.X,
                Y = h.Y,
                Name = h.Name,
                Text = h.Text,
                Index = h.Index,
                SecondLevelId = h.SecondLevelId,
                About = new AboutViewModel()
                {
                    Title = new TitleViewModel()
                    {
                        Img = _fileManager.GetFileLinksByName(7, horisontalGuid, h.About!.Title!.Img!.Main!).FirstOrDefault(),
                        Name = h.About!.Title!.Name,
                        TitleName = h.About.Title.TitleName,
                        Number = h.About.Title.Number
                    },
                    Main = new MainViewModel()
                    {
                        Img = _fileManager.GetFileLinksByName(8, horisontalGuid, h.About.Main!.Img!.Main!).FirstOrDefault(),
                        Title = h.About.Main!.Title
                    },
                    Text = h.About.Text,
                    Number = h.About.Number,
                    Background = _fileManager.GetFileLinksByName(3, horisontalGuid, h.About.Background!.Main!).FirstOrDefault(),
                    Images = resultImage
                }
            }).FirstOrDefault();

            return responce!;
        }

        public async Task<List<HorisontalViewModel>> GetHorisontalAll()
        {
            var responce = _horisontal.TableNoTracking
                .Include(h => h.Img)
                .Include(h => h.About)
                .Include(h => h.About!.Background)
                .Include(h => h.About!.Title)
                .Include(h => h.About!.Main)
                .Include(h => h.About!.Main!.Img)
                .Include(h => h.About!.Title!.Img)
                .ToList()
                .Select(h => new HorisontalViewModel()
                {
                    Id = h.Id,
                    Img = _fileManager.GetFileLinksByName(1, h.Id.ToString(), h.Img!.Main!).FirstOrDefault(),
                    X = h.X,
                    Y = h.Y,
                    Name = h.Name,
                    Text = h.Text,
                    Index = h.Index,
                    SecondLevelId = h.SecondLevelId,
                    About = new AboutViewModel()
                    {
                        Title = new TitleViewModel()
                        {
                            Img = _fileManager.GetFileLinksByName(7, h.Id.ToString(), h.About!.Title!.Img!.Main!).FirstOrDefault(),
                            Name = h.About!.Title!.Name,
                            TitleName = h.About.Title.TitleName,
                            Number = h.About.Title.Number
                        },
                        Main = new MainViewModel()
                        {
                            Img = _fileManager.GetFileLinksByName(8, h.Id.ToString(), h.About.Main!.Img!.Main!).FirstOrDefault(),
                            Title = h.About.Main!.Title
                        },
                        Text = h.About.Text,
                        Number = h.About.Number,
                        Background = _fileManager.GetFileLinksByName(3, h.Id.ToString(), h.About.Background!.Main!).FirstOrDefault(),
                        Images = h.About.Images!.Where(i => _fileManager.GetFileLinksByName(4, h.Id.ToString(), i.Main!).FirstOrDefault() != null).Select(i => new ImageLink
                        {
                            Link = _fileManager.GetFileLinksByName(4, h.Id.ToString(), i.Main!).FirstOrDefault(),
                            Name = i.Main,
                            Priority = i.Priority,
                            Id = i.Id
                        }).OrderBy(i => i.Priority).ToList()
                    }
                }).ToList();

            return responce!;
        }


        public async Task<List<HorisontalListModel>> GetHorisontalList()
        {
            var responce = await _horisontal.TableNoTracking.Where(h => h.SecondLevelId == null).Select(h => new HorisontalListModel
            {
                Id = h.Id,
                Name = h.Name
            }).ToListAsync();

            return responce;
        }

        public async Task<BaseModel> UpdateHorisontal(Domain.RequestModel.HorisontalRequestModel? updateModel, long? secondLevelId)
        {
            try
            {
                if (updateModel == null)
                    throw new ApiException("Не удалось отредактировать Horisontal. Пустой запрос.");

                var horisontalGuid = updateModel.Id.ToString();

                List<Image> listImages = new List<Image>();

                if (updateModel.About != null && updateModel.About!.Images! != null)
                {
                    foreach (var elem in updateModel.About!.Images!)
                    {
                        var image = _horisontal.TableNoTracking.Where(h => h.Id == updateModel.Id)
                        .Select(h => h.About!.Images!.Where(i => elem.Id == i.Id).Select(i => new Image
                        {
                            Id = i.Id,
                            Link = elem.Link ?? i.Link,
                            Main = (elem.FromDataFile! == null ? null : elem.FromDataFile!.Select(f => f.FileName).FirstOrDefault()) ?? i.Main,
                            Priority = elem.Priority ?? i.Priority,
                        }).FirstOrDefault()).FirstOrDefault();

                        if (image != null)
                        {
                            listImages.Add(image);
                        }
                        else
                        {
                            Image newImage = new Image
                            {
                                Link = elem.Link,
                                Main = elem.FromDataFile! == null ? null : elem.FromDataFile!.Select(f => f.FileName).FirstOrDefault(),
                                Priority = elem.Priority
                            };

                            listImages.Add(newImage);
                        }
                    }
                }

                List<long> idImagesList = listImages.Select(l => l.Id).ToList();

                List<Image> deleteImages = _horisontal.TableNoTracking.Where(h => h.Id == updateModel.Id).Select(h => h.About!.Images!.Where(i => !idImagesList.Any(l => l == i.Id)).ToList()).FirstOrDefault()!;

                var responce = _horisontal.TableNoTracking.Where(h => h.Id == updateModel.Id).Select(h => new Data.Entities.Horisontal()
                {
                    Id = h.Id,
                    X = updateModel.X ?? h.X,
                    Y = updateModel.Y ?? h.Y,
                    Index = updateModel.Index ?? h.Index,
                    Img = updateModel.Img == null ? h.Img : new Image
                    {
                        Id = h.Id,
                        Main = (updateModel.Img!.FromDataFile! == null ? null : updateModel.Img!.FromDataFile!.Select(i => i.FileName).FirstOrDefault()) ?? h.Img!.Main,
                        Link = updateModel.Img.Link ?? h.Img!.Link,
                    },
                    Text = updateModel.Text ?? h.Text,
                    Name = updateModel.Name ?? h.Name,
                    SecondLevelId = secondLevelId ?? updateModel.SecondLevelId ?? h.SecondLevelId,
                    About = updateModel.About == null ? h.About : new Data.Entities.About()
                    {
                        Id = h.About!.Id, 
                        Title = updateModel.About.Title == null ? h.About!.Title : new Data.Entities.Title()
                        {
                            Id = h.About.Title!.Id,
                            Name = updateModel.About!.Title!.Name ?? h.About!.Title!.Name,
                            TitleName = updateModel.About.Title.TitleName ?? h.About!.Title!.TitleName,
                            Number = updateModel.About.Title.Number ?? h.About!.Title!.Number,
                            Img = updateModel.About.Title.Img == null ? h.About!.Title!.Img : new Image
                            {
                                Id = h.About.Title.Img!.Id,
                                Main = (updateModel.About.Title.Img!.FromDataFile! == null ? null : updateModel.About.Title.Img!.FromDataFile!.Select(i => i.FileName).FirstOrDefault()) ?? h.About!.Title!.Img!.Main,
                                Link = updateModel.About.Title.Img.Link ?? h.About!.Title!.Img!.Link
                            }
                        },
                        Main = updateModel.About.Main == null ? h.About!.Main : new Data.Entities.Main()
                        {
                            Id = h.About.Main!.Id,
                            Img = updateModel.About.Main.Img == null ? h.About!.Main!.Img : new Image
                            {
                                Id = h.About.Main.Img!.Id,
                                Main = (updateModel.About.Main!.Img!.FromDataFile! == null ? null : updateModel.About.Main!.Img!.FromDataFile!.Select(i => i.FileName).FirstOrDefault()) ?? h.About!.Main!.Img!.Main,
                                Link = updateModel.About.Main!.Img!.Link ?? h.About!.Main!.Img!.Link
                            },
                            Title = updateModel.About.Main.Title ?? h.About!.Main!.Title
                        },
                        Text = updateModel.About.Text ?? h.About!.Text,
                        Number = updateModel.About.Number ?? h.About!.Number,
                        Background = updateModel.About.Background == null ? h.About!.Background : new Image
                        {
                            Id = h.About.Background!.Id,
                            Main = (updateModel.About.Background!.FromDataFile! == null ? null : updateModel.About.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault()) ?? h.About!.Background!.Main,
                            Link = updateModel.About.Background.Link ?? h.About!.Background!.Link
                        },
                        Images = listImages != null ? listImages : h.About.Images
                    }
                }).FirstOrDefault();

                using var transaction = _horisontal.DbContext.Database.BeginTransaction();

                if (updateModel.About != null && updateModel.About!.Images != null)
                    foreach (ImageRequestModel file in updateModel.About!.Images!)
                    {
                        if (file.FromDataFile != null && _fileManager.GetFileLinksByName(4, horisontalGuid, file.FromDataFile.Select(f => f.FileName).FirstOrDefault()!).FirstOrDefault() == null)
                            await SaveFiles(file.FromDataFile!, updateModel.Id, "AboutImages");
                    }

                if (updateModel.About != null && updateModel.About.Background != null && updateModel.About.Background!.FromDataFile != null && _fileManager.GetFileLinksByName(3, horisontalGuid, responce!.About!.Background!.Main!).FirstOrDefault() == null)
                    await SaveFiles(updateModel.About.Background.FromDataFile!, updateModel.Id, "AboutBackground");

                if (updateModel.About != null && updateModel.About!.Title != null && updateModel.About!.Title!.Img != null && updateModel.About!.Title!.Img!.FromDataFile != null && _fileManager.GetFileLinksByName(7, horisontalGuid, responce!.About!.Title!.Img!.Main!).FirstOrDefault() == null)
                    await SaveFiles(updateModel.About.Title.Img.FromDataFile!, updateModel.Id, "TitleImg");

                if (updateModel.Img != null && updateModel.Img!.FromDataFile != null && _fileManager.GetFileLinksByName(1, horisontalGuid, responce!.Img!.Main!).FirstOrDefault() == null)
                    await SaveFiles(updateModel.Img.FromDataFile!, updateModel.Id, "HorisontalImg");

                if (updateModel.About != null && updateModel.About!.Main != null && updateModel.About.Main!.Img != null && updateModel.About.Main!.Img!.FromDataFile != null && _fileManager.GetFileLinksByName(8, horisontalGuid, responce!.About!.Main!.Img!.Main!).FirstOrDefault() == null)
                    await SaveFiles(updateModel.About.Main.Img.FromDataFile!, updateModel.Id, "MainImg");

                await _horisontal.Update(responce!);

                await _image.Delete(deleteImages!);

                transaction.Commit();

                return new BaseModel { result = true, Value = updateModel!.Id };
            }
            catch (Exception ex)
            {
                throw new ApiException($"Произошла ошибка {ex}");
            }
        }

        public async Task<BaseModel> InsertHorisontal(Domain.RequestModel.HorisontalRequestModel? insertModel, long? secondLevelId = null)
        {
            try
            {
                if (insertModel == null)
                    throw new ApiException("Не удалось Добавить Horisontal. Пустой запрос.");

                var responce = new Data.Entities.Horisontal()
                {
                    Id = insertModel.Id,
                    X = insertModel.X,
                    Y = insertModel.Y,
                    Index = insertModel.Index,
                    Img = insertModel.Img == null ? null : new Image
                    {
                        Main = insertModel.Img!.FromDataFile! == null ? null : insertModel.Img!.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                    },
                    Text = insertModel.Text,
                    Name = insertModel.Name,
                    SecondLevelId = secondLevelId ?? insertModel.SecondLevelId,
                    About = insertModel.About == null ? null : new Data.Entities.About()
                    {
                        Title = insertModel.About!.Title == null ? null : new Data.Entities.Title()
                        {
                            Name = insertModel.About!.Title!.Name,
                            TitleName = insertModel.About.Title.TitleName,
                            Number = insertModel.About.Title.Number,
                            Img = insertModel.About.Title.Img == null ? null : new Image
                            {
                                Main = insertModel.About.Title.Img!.FromDataFile! == null ? null : insertModel.About.Title.Img!.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                            }
                        },
                        Text = insertModel.About.Text,
                        Number = insertModel.About.Number,
                        Main = insertModel.About.Main == null ? null : new Main()
                        {
                            Img = insertModel.About.Main.Img == null ? null : new Image
                            {
                                Main = insertModel.About.Background!.FromDataFile! == null ? null : insertModel.About.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                            },
                            Title = insertModel.About.Main!.Title
                        },
                        Background = insertModel.About.Background == null ? null : new Image
                        {
                            Main = insertModel.About.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                        },
                        Images = insertModel.About.Images == null ? null : insertModel.About.Images!.Select(i => new Image
                        {
                            Main = i.FromDataFile! == null ? null : i.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                            Preview = i.FromDataFilePreview! == null ? null : i.FromDataFilePreview!.Select(i => i.FileName).FirstOrDefault(),
                            Priority = i.Priority
                        }).ToList()
                    }
                };

                using var transaction = _horisontal.DbContext.Database.BeginTransaction();

                await _horisontal.Insert(responce);

                foreach (Domain.RequestModel.ImageRequestModel img in insertModel.About!.Images!)
                {
                    if (img.FromDataFile != null)
                        await SaveFiles(img.FromDataFile!, responce.Id, "AboutImages");

                    if (img.FromDataFilePreview != null)
                        await SaveFiles(img.FromDataFilePreview!, responce.Id, "PrewiewImages");
                }

                if (insertModel.About != null && insertModel.About.Title != null && insertModel.About.Title!.Img != null && insertModel.About.Title!.Img.FromDataFile != null)
                    await SaveFiles(insertModel.About.Title.Img.FromDataFile!, responce.Id, "TitleImg");

                if (insertModel.About != null && insertModel.About!.Main != null && insertModel.About!.Main!.Img != null && insertModel.About!.Main!.Img!.FromDataFile != null)
                    await SaveFiles(insertModel.About.Main.Img!.FromDataFile!, responce.Id, "MainImg");

                if (insertModel.Img != null && insertModel.Img.FromDataFile != null)
                    await SaveFiles(insertModel.Img.FromDataFile!, responce.Id, "HorisontalImg");

                if (insertModel.About != null && insertModel.About!.Background != null && insertModel.About!.Background.FromDataFile != null)
                    await SaveFiles(insertModel.About!.Background!.FromDataFile!, responce.Id, "AboutBackground");

                transaction.Commit();

                return new BaseModel { result = true, Value = responce.Id };
            }
            catch (Exception ex)
            {
                throw new ApiException($"Произошла ошибка {ex}");
            }
        }

        public async Task<BaseModel> DeleteHorisontal(long? horisontalId)
        {
            if (horisontalId == null)
                throw new ApiException("Не удалось удалить horisontal. Пустой запрос.");

            var horisontal = _horisontal.Table.Where(x => x.Id == horisontalId && x.IsDeleted == false);

            if (!horisontal.Any())
                throw new ApiException(
                    "Не удалось удалить horisontal. Указанный horisontal не найден либо удален."
                );

            await DataService.Delete(horisontal, _horisontal);

            await DeleteFiles(horisontal.FirstOrDefault()!.Id);

            return new BaseModel { result = true, Value = horisontal.FirstOrDefault()!.Id };
        }

        private async Task DeleteFiles(long Id, long? parametrId = null, string? name = null)
        {
            var Images = _file.TableNoTracking.Include(y => y.DataType).Where(y =>
                 y.ElementId == Id.ToString() && !y.IsDeleted && (parametrId == null ? !y.IsDeleted : y.DataTypeId == parametrId) && (name == null ? !y.IsDeleted : y.Name == name)
            );

            if (!Images.Any())
                return;

            foreach (var item in Images)
            {
                await _fileManager.SaveOrDeleteFileAsync(
                    "D",
                    new FileRequestModel(
                        id: Id.ToString(),
                        type: item.DataType.Alias,
                        name: item.Name,
                        flag: "D"
                    )
                );
            }
        }

        private async Task SaveFiles(IFormFileCollection File, long Id, string alias)
        {
                await _fileManager.SaveOrDeleteFileAsync(
                    "I",
                    new FileRequestModel(
                        id: Id.ToString(),
                        type: alias,
                        name: null,
                        flag: "I"
                    )
                    {
                        Files = File.Select(f => new FileModel
                            {
                                Name = f.FileName,
                                Extention = f.ContentType,
                                DataType = "2"
                            }).ToList()
                    },
                    null!,
                    File
                );
        }
    }
}
