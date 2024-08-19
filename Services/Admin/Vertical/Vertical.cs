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
using Services.Admin.Horisontal;

namespace Services.Admin.Vertical
{
    public class Vertical : IVertical
    {
        private readonly IAppData<Data.Entities.Vertical> _vertical;
        private readonly IFileManager _fileManager;
        private readonly IAppData<BaseFile> _file;
        private readonly IAppData<DataType> _dataType;
        private readonly IHorisontal _horisontal;


        public Vertical(
               IAppData<Data.Entities.Vertical> vertical,
               IFileManager fileManager,
               IAppData<BaseFile> file,
               IAppData<DataType> dataType,
               IHorisontal horisontal
        )
        {
            _vertical = vertical;
            _fileManager = fileManager;
            _file = file;
            _dataType = dataType;
            _horisontal = horisontal;
        }

        public async Task<VerticalViewModel> GetVertical(long? verticalId, long? periodId)
        {
            if (verticalId == null)
                throw new ApiException("Не указан идентификатор для vertical");

            var verticalGuid = _vertical.TableNoTracking.Where(v => v.GroupId == verticalId && v.PeriodId == periodId).Select(v => v.Id).FirstOrDefault().ToString();

            var request = _vertical.TableNoTracking.Where(v => v.GroupId == verticalId && v.PeriodId == periodId).Select(v => v.SecondLevel!.Sources).FirstOrDefault();

            var source = await HorisontalListForVertical(request!);

            if (source.Any())
            {
                var test = source.OrderBy(x => x.Index);
                source = test.ToList();
            }

            var responce = _vertical.TableNoTracking.Where(v => v.GroupId == verticalId && v.PeriodId == periodId)
                .Include(v => v.Interval)
                .Include(v => v.Header)
                .Include(v => v.Map)
                .Include(v => v.Map!.Background)
                .Include(v => v.SecondLevel)
                .Include(v => v.SecondLevel!.Header)
                .Include(v => v.SecondLevel!.Background)
                .Include(v => v.SecondLevel!.ThirdLevelBackground)
                .Include(v => v.SecondLevel!.Map)
                .Include(v => v.SecondLevel!.Map!.Background)
                .ToList()
                .Select(v => new VerticalViewModel()
                {
                    Id = v.Id,
                    GroupId = v.GroupId!.Value,
                    PeriodId = v.PeriodId!.Value,
                    Interval = new Interval()
                    {
                        Start = v.Interval!.Start,
                        End = v.Interval.End
                    },
                    Header = new Header()
                    {
                        Title = v.Header!.Title,
                        Description = v.Header.Description
                    },
                    Map = new MapViewModel()
                    {
                        Background = _fileManager.GetFileLinksByName(2, verticalGuid, v.Map!.Background!.Main!).FirstOrDefault(),
                    },
                    Text = v.Text,
                    SecondLevel = new SecondLevelViewModel()
                    {
                        id = v.SecondLevel!.Id,
                        Header = new Header()
                        {
                            Title = v.SecondLevel!.Header!.Title,
                            Description = v.SecondLevel.Header.Description
                        },
                        Background = _fileManager.GetFileLinksByName(5, verticalGuid, v.SecondLevel!.Background!.Main!).FirstOrDefault(),
                        ThirdLevelBackground = _fileManager.GetFileLinksByName(6, verticalGuid, v.SecondLevel!.ThirdLevelBackground!.Main!).FirstOrDefault(),
                        Map = new MapViewModel()
                        {
                            Background = _fileManager.GetFileLinksByName(2, verticalGuid, v.SecondLevel!.Map!.Background!.Main!).FirstOrDefault(),
                        },
                        Text = v.SecondLevel!.Text,
                        Sources = source.Any() ? source : null
                    }
                }).FirstOrDefault();

            return responce!;
        }

        public async Task<List<VerticalAllViewModel>> GetVerticalAll()
        {
            List<VerticalAllViewModel> responce = new List<VerticalAllViewModel>();

            foreach (var elem in _vertical.TableNoTracking.Where(v => !v.IsDeleted)
                .Include(v => v.Interval)
                .Include(v => v.Header)
                .Include(v => v.Map)
                .Include(v => v.Map!.Background)
                .Include(v => v.SecondLevel)
                .Include(v => v.SecondLevel!.Header)
                .Include(v => v.SecondLevel!.Background)
                .Include(v => v.SecondLevel!.ThirdLevelBackground)
                .Include(v => v.SecondLevel!.Map)
                .Include(v => v.SecondLevel!.Map!.Background)
                .Include(v => v.SecondLevel!.Sources)
                .ToList())
            {
                var verticalGuid = elem.Id.ToString();

                var source = await HorisontalListForVerticalList(elem.SecondLevel!.Sources!);

                if (source.Any())
                {
                    var test = source.OrderBy(x => x.Index);
                    source = test.ToList();
                }

                var result = new VerticalAllViewModel()
                {
                    Id = elem.Id,
                    Interval = new Interval()
                    {
                        Start = elem.Interval!.Start,
                        End = elem.Interval.End
                    },
                    Header = new Header()
                    {
                        Title = elem.Header!.Title,
                        Description = elem.Header.Description
                    },
                    Map = new MapViewModel()
                    {
                        Background = _fileManager.GetFileLinksByName(2, verticalGuid, elem.Map!.Background!.Main!).FirstOrDefault(),
                    },
                    Text = elem.Text,
                    SecondLevel = new SecondLevelAllViewModel()
                    {
                        id = elem.SecondLevel!.Id,
                        Header = new Header()
                        {
                            Title = elem.SecondLevel!.Header!.Title,
                            Description = elem.SecondLevel.Header.Description
                        },
                        Background = _fileManager.GetFileLinksByName(5, verticalGuid, elem.SecondLevel!.Background!.Main!).FirstOrDefault(),
                        ThirdLevelBackground = _fileManager.GetFileLinksByName(6, verticalGuid, elem.SecondLevel!.ThirdLevelBackground!.Main!).FirstOrDefault(),
                        Map = new MapViewModel()
                        {
                            Background = _fileManager.GetFileLinksByName(2, verticalGuid, elem.SecondLevel!.Map!.Background!.Main!).FirstOrDefault(),
                        },
                        Text = elem.SecondLevel!.Text,
                        Sources = source.Any() ? source : null
                    }
                };

                responce.Add(result);
            }

            return responce!;
        }

        public async Task<List<VerticalListModel>> GetVerticalList()
        {
            var responce = await _vertical.TableNoTracking.Select(v => new VerticalListModel
            {
                Id = v.GroupId,
                Name = v.GroupId.ToString(),
            }).Distinct().ToListAsync();

            return responce;
        }

        public async Task<BaseModel> UpdateVertical(Domain.RequestModel.VerticalRequestModel? updateModel)
        {
            try
            {
                if (updateModel == null)
                    throw new ApiException("Не удалось отредактировать Vertical. Пустой запрос.");

                var verticalGuid = updateModel.Id.ToString();

                var thirdLevelBackground = _fileManager.GetFileLink(6, verticalGuid).ToList();

                var secondLevelBackground = _fileManager.GetFileLink(5, verticalGuid).ToList();

                var responce = _vertical.TableNoTracking.Where(v => v.Id == updateModel.Id).Select(v => new Data.Entities.Vertical()
                {
                    Id = v.Id,
                    GroupId = v.GroupId,
                    PeriodId = v.PeriodId,
                    DateUpdate = DateTime.UtcNow,
                    Interval = updateModel.Interval == null ? v.Interval : new Interval()
                    {
                        Id = v.Interval!.Id,
                        Start = updateModel.Interval!.Start ?? v.Interval!.Start,
                        End = updateModel.Interval.End ?? v.Interval!.End
                    },
                    Header = updateModel.Header == null ? v.Header : new Header()
                    {
                        Id = v.Header!.Id,
                        Title = updateModel.Header!.Title ?? v.Header!.Title,
                        Description = updateModel.Header.Description ?? v.Header!.Description
                    },
                    Map = updateModel.Map == null ? v.Map : new Map()
                    {
                        Id = v.Map!.Id,
                        Background = updateModel.Map.Background == null ? v.Map!.Background : new Image
                        {
                            Id = v.Map.Background!.Id,
                            Main = (updateModel.Map.Background.FromDataFile! == null ? null : updateModel.Map!.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault()) ?? v.Map!.Background!.Main,
                            Link = updateModel.Map.Background.Link ?? v.Map!.Background!.Link,
                        }
                    },
                    Text = updateModel.Text ?? v.Text,
                    SecondLevel = updateModel.SecondLevel == null ? v.SecondLevel : new SecondLevel()
                    {
                        Id = v.SecondLevel!.Id,
                        Header = updateModel.SecondLevel.Header == null ? v.SecondLevel!.Header : new Header()
                        {
                            Id = v.SecondLevel.Header!.Id,
                            Title = updateModel.SecondLevel.Header!.Title ?? v.SecondLevel!.Header!.Title,
                            Description = updateModel.SecondLevel.Header.Description ?? v.SecondLevel!.Header!.Description
                        },
                        Background = updateModel.SecondLevel.Background == null ? v.SecondLevel!.Background : new Image
                        {
                            Id = v.SecondLevel.Background!.Id,
                            Main = (updateModel.SecondLevel.Background.FromDataFile! == null ? null : updateModel.SecondLevel!.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault()) ?? v.SecondLevel!.Background!.Main,
                            Link = updateModel.SecondLevel.Background.Link ?? v.SecondLevel!.Background!.Link,
                        },
                        ThirdLevelBackground = updateModel.SecondLevel.ThirdLevelBackground == null ? v.SecondLevel!.ThirdLevelBackground : new Image
                        {
                            Id = v.SecondLevel.ThirdLevelBackground!.Id,
                            Main = (updateModel.SecondLevel.ThirdLevelBackground!.FromDataFile! == null ? null : updateModel.SecondLevel!.ThirdLevelBackground!.FromDataFile!.Select(i => i.FileName).FirstOrDefault()) ?? v.SecondLevel!.ThirdLevelBackground!.Main,
                            Link = updateModel.SecondLevel.ThirdLevelBackground.Link ?? v.SecondLevel!.ThirdLevelBackground!.Link,
                        },
                        Map = updateModel.SecondLevel.Map == null ? v.SecondLevel!.Map : new Map()
                        {
                            Id = v.SecondLevel.Map!.Id,
                            Background = updateModel.SecondLevel.Map.Background == null ? v.SecondLevel!.Map!.Background : new Image
                            {
                                Id = v.SecondLevel.Map.Background!.Id,
                                Main = (updateModel.SecondLevel.Map.Background.FromDataFile! == null ? null : updateModel.SecondLevel.Map!.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault()) ?? v.SecondLevel!.Map!.Background!.Main,
                                Link = updateModel.SecondLevel.Map.Background.Link ?? v.SecondLevel!.Map!.Background!.Link,
                            },
                        },
                        Text = updateModel.SecondLevel!.Text ?? v.SecondLevel!.Text,
                    }
                }).FirstOrDefault();

                //using var transaction = _vertical.DbContext.Database.BeginTransaction();

                if (updateModel.Map != null && updateModel.Map!.Background != null && updateModel.Map!.Background!.FromDataFile != null && _fileManager.GetFileLinksByName(2, verticalGuid, responce!.Map!.Background!.Main!).FirstOrDefault() == null)
                    await SaveFiles(updateModel.Map.Background.FromDataFile!, updateModel.Id, "MapBackground");

                if (updateModel.SecondLevel != null && updateModel.SecondLevel!.Map != null && updateModel.SecondLevel!.Map!.Background != null && updateModel.SecondLevel!.Map!.Background!.FromDataFile != null && _fileManager.GetFileLinksByName(2, verticalGuid, responce!.Map!.Background!.Main!).FirstOrDefault() == null)
                    await SaveFiles(updateModel.SecondLevel.Map.Background.FromDataFile!, updateModel.Id, "MapBackground");

                if (updateModel.SecondLevel != null && updateModel.SecondLevel!.Background != null && updateModel.SecondLevel!.Background!.FromDataFile != null && _fileManager.GetFileLinksByName(5, verticalGuid, responce!.SecondLevel!.Background!.Main!).FirstOrDefault() == null)
                    await SaveFiles(updateModel.SecondLevel.Background.FromDataFile!, updateModel.Id, "SecondLevelBackground");

                if (updateModel.SecondLevel != null && updateModel.SecondLevel!.ThirdLevelBackground != null && updateModel.SecondLevel.ThirdLevelBackground!.FromDataFile != null && _fileManager.GetFileLinksByName(6, verticalGuid, responce!.SecondLevel!.ThirdLevelBackground!.Main!).FirstOrDefault() == null)
                    await SaveFiles(updateModel.SecondLevel.ThirdLevelBackground.FromDataFile!, updateModel.Id, "ThirdLevelBackground");

                await _vertical.Update(responce!);

                //transaction.Commit();

                return new BaseModel { result = true, Value = responce!.Id };
            }
            catch (Exception ex)
            {
                throw new ApiException($"Произошла ошибка {ex}");
            }
        }

        public async Task<BaseModel> InsertVertical(Domain.RequestModel.VerticalRequestModel? insertModel)
        {
            try
            {
                if (insertModel == null)
                    throw new ApiException("Не удалось Добавить Vertical. Пустой запрос.");

                var responce = new Data.Entities.Vertical()
                {
                    Interval = insertModel.Interval == null ? null : new Interval()
                    {
                        Start = insertModel.Interval!.Start,
                        End = insertModel.Interval.End
                    },
                    Header = insertModel.Header == null ? null : new Header()
                    {
                        Title = insertModel.Header!.Title,
                        Description = insertModel.Header.Description
                    },
                    Map = insertModel.Map == null ? null : new Map()
                    {
                        Background = insertModel.Map.Background == null ? null : new Image
                        {
                            Main = insertModel.Map!.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                        }
                    },
                    Text = insertModel.Text,
                    SecondLevel = insertModel.SecondLevel == null ? null : new SecondLevel()
                    {
                        Header = insertModel.SecondLevel.Header == null ? null : new Header()
                        {
                            Title = insertModel.Header!.Title,
                            Description = insertModel.Header.Description
                        },
                        Background = insertModel.SecondLevel.Background == null ? null : new Image
                        {
                            Main = insertModel.SecondLevel!.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                        },
                        ThirdLevelBackground = insertModel.SecondLevel.ThirdLevelBackground == null ? null : new Image
                        {
                            Main = insertModel.SecondLevel!.ThirdLevelBackground!.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                        },
                        Map = insertModel.SecondLevel.Map == null ? null : new Map()
                        {
                            Background = insertModel.SecondLevel.Background == null ? null : new Image
                            {
                                Main = insertModel.SecondLevel.Map!.Background!.FromDataFile!.Select(i => i.FileName).FirstOrDefault(),
                            },
                        },
                        Text = insertModel.SecondLevel!.Text,
                    }
                };

                using var transaction = _vertical.DbContext.Database.BeginTransaction();

                await _vertical.Insert(responce);

                if (insertModel.Map != null && insertModel!.Map!.Background != null && insertModel.Map!.Background!.FromDataFile != null)
                    await SaveFiles(insertModel.Map!.Background!.FromDataFile!, responce.Id, "MapBackground");

                if (insertModel.SecondLevel != null && insertModel.SecondLevel!.Map != null && insertModel.SecondLevel!.Map!.Background != null && insertModel.SecondLevel!.Map!.Background!.FromDataFile != null)
                    await SaveFiles(insertModel.SecondLevel!.Map!.Background!.FromDataFile!, responce.Id, "MapBackground");

                if (insertModel.SecondLevel != null && insertModel.SecondLevel!.Background != null && insertModel.SecondLevel!.Background.FromDataFile != null)
                    await SaveFiles(insertModel.SecondLevel!.Background!.FromDataFile!, responce.Id, "SecondLevelBackground");

                if (insertModel.SecondLevel != null && insertModel.SecondLevel!.ThirdLevelBackground != null && insertModel.SecondLevel!.ThirdLevelBackground!.FromDataFile != null)
                    await SaveFiles(insertModel.SecondLevel!.ThirdLevelBackground!.FromDataFile!, responce.Id, "ThirdLevelBackground");

                transaction.Commit();

                if (insertModel.SecondLevel != null && insertModel.SecondLevel.Sources != null)
                {
                    foreach (var horisontal in insertModel.SecondLevel!.Sources!)
                    {
                        await _horisontal.InsertHorisontal(horisontal, responce.SecondLevel!.Id);
                    }
                }

                return new BaseModel { result = true, Value = responce.Id };
            }
            catch (Exception ex)
            {
                throw new ApiException($"Произошла ошибка {ex}");
            }
        }

        public async Task<BaseModel> DeleteVertical(long? verticalId)
        {
            if (verticalId == null)
                throw new ApiException("Не удалось удалить vertical. Пустой запрос.");

            var vertical = _vertical.Table.Where(x => x.Id == verticalId && x.IsDeleted == false);

            if (!vertical.Any())
                throw new ApiException(
                    "Не удалось удалить vertical. Указанный vertical не найден либо удален."
                );

            await DataService.Delete(vertical, _vertical);

            await DeleteFiles(vertical.FirstOrDefault()!.Id);

            return new BaseModel { result = true, Value = vertical.FirstOrDefault()!.Id };
        }

        private async Task<List<HorisontalViewModel>> HorisontalListForVertical(List<Data.Entities.Horisontal> list)
        {
            List<HorisontalViewModel> responce = new List<HorisontalViewModel>();

            foreach (var horisontal in list!)
            {
                var elem = _horisontal.GetHorisontal(horisontal.Id);

                responce.Add(await elem);
            }

            return responce;
        }

        private async Task<List<HorisontalForVerticalViewModel>> HorisontalListForVerticalList(List<Data.Entities.Horisontal> list)
        {
            List<HorisontalForVerticalViewModel> responce = new List<HorisontalForVerticalViewModel>();

            foreach (var horisontal in list!)
            {
                var elem = _horisontal.GetHorisontalForSource(horisontal.Id);

                responce.Add(await elem);
            }

            return responce;
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
