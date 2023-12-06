using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EWI_System.WebAPI.Controllers
{
    /// <summary>
    /// 口层
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class WipStorageController : ControllerBase
    {
        #region 注入服务层
        /// <summary>
        /// 服务层
        /// </summary>
        public IWipStorageService wipStorageService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="wipStorageService"></param>
        public WipStorageController(IWipStorageService wipStorageService)
        {
            this.wipStorageService = wipStorageService;
        }
        #endregion

        #region fectchLocationArea
        /// <summary>
        /// getLocationArea
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R fectchLocationArea()
        {
            return R.OK(wipStorageService.fectchLocationArea());
        }
        #endregion

        #region Magazine
        /// <summary>
        ///分页查询
        ///pageNum: 1,
        ///pageSize: 10,
        ///keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R fetchMagazineList(int pageNum, int pageSize, string keyword)
        {
            int total = 0;
            var magazineList = wipStorageService.fetchMagazineList(pageNum, pageSize, keyword, ref total);
            return R.OK(magazineList).data("total", total);
        }
        #endregion

        #region wipcar
        /// <summary>
        /// add
        /// </summary>
        /// <param name="wipCar"></param>
        /// <returns></returns>
        [HttpPost]
        public R createCar(WipCar wipCar)
        {
            if (string.IsNullOrEmpty(wipCar.wipCarNo))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (wipStorageService.fetchWipCarByNo(wipCar.wipCarNo) > 0)
            {
                return R.Error(@"新增失败,已经存在该编号的库位,已存在库位" + wipCar.wipCarNo);
            }
            if (wipStorageService.createCar(wipCar))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }
        /// <summary>
        /// batchAdd
        /// </summary>
        /// <param name="carList"></param>
        /// <returns></returns>
        [HttpPost]
        public R createBatchCar(List<WipCar> carList)
        {
            if (carList.Count < 0)
            {
                return R.Error("批量新增失败，请检查提交的数据");
            }
            string No = string.Empty;
            foreach (var item in carList)
            {
                if (wipStorageService.fetchLoctionByNo(item.wipCarNo) > 0)
                {
                    No += ("__" + item.wipCarNo);
                    continue;
                }
            }
            if (!string.IsNullOrEmpty(No))
            {
                return R.Error(@"批量新增失败，已经存在该编号的库位,已存在库位" + No);
            }
            if (wipStorageService.createBatchCar(carList))
            {
                return R.OK();
            };
            return R.Error("批量新增失败，请检查提交的数据");
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="wipCarId"></param>
        /// <returns></returns>
        [HttpGet]
        public R deleteCar(int wipCarId)
        {
            if (wipStorageService.deleteCar(wipCarId, out string msg))
            {
                return R.OK();
            };
            return R.Error(msg);
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="wipCar"></param>
        /// <returns></returns>
        [HttpPost]
        public R updateCar(WipCar wipCar)
        {
            if (string.IsNullOrEmpty(wipCar.wipCarNo))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (wipStorageService.updateCar(wipCar))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");
        }
        /// <summary>
        /// updateStatus
        /// </summary>
        /// <param name="wipCar"></param>
        /// <returns></returns>
        [HttpPost]
        public R updateCarStatus(WipCar wipCar)
        {
            if (wipStorageService.updateCarStatus(wipCar))
            {
                return R.OK();
            }
            return R.Error("修改状态失败，请联系管理员");
        }
        /// <summary>
        ///分页查询
        ///pageNum: 1,
        ///pageSize: 10,
        ///keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R fetchCarList(int pageNum, int pageSize, string keyword)
        {
            int total = 0;
            var wipCarlist = wipStorageService.fetchCarList(pageNum, pageSize, keyword, ref total);
            return R.OK(wipCarlist).data("total", total);
        }
        #endregion

        #region location
        /// <summary>
        /// add
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        public R createLocation(WipStorageLocation location)
        {
            if (string.IsNullOrEmpty(location.LocationNo))
            {
                return R.Error("新增失败，请检查提交的数据");
            }
            if (wipStorageService.fetchLoctionByNo(location.LocationNo) > 0)
            {
                return R.Error(@"新增失败,已经存在该编号的库位,已存在库位" + location.LocationNo);
            }
            if (wipStorageService.createLocation(location))
            {
                return R.OK();
            };
            return R.Error("新增失败，请检查提交的数据");
        }
        /// <summary>
        /// batchAdd
        /// </summary>
        /// <param name="locationList"></param>
        /// <returns></returns>
        [HttpPost]
        public R createBatchLocation(List<WipStorageLocation> locationList)
        {
            if (locationList.Count < 0)
            {
                return R.Error("批量新增失败，请检查提交的数据");
            }
            string No = string.Empty;
            foreach (var item in locationList)
            {
                if (wipStorageService.fetchLoctionByNo(item.LocationNo) > 0)
                {
                    No += ("__" + item.LocationNo);
                    continue;
                }
            }
            if (!string.IsNullOrEmpty(No))
            {
                return R.Error(@"批量新增失败，已经存在该编号的库位,已存在库位" + No);
            }
            if (wipStorageService.createBatchLocation(locationList))
            {
                return R.OK();
            };
            return R.Error("批量新增失败，请检查提交的数据");
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="wipStorageLocationId"></param>
        /// <returns></returns>
        [HttpGet]
        public R deleteLocation(int wipStorageLocationId)
        {
            if (wipStorageService.deleteLocation(wipStorageLocationId, out string msg))
            {
                return R.OK();
            };
            return R.Error(msg);
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        public R updateLocation(WipStorageLocation location)
        {
            if (string.IsNullOrEmpty(location.LocationNo))
            {
                return R.Error("更新失败，请检查提交的数据");
            }
            if (wipStorageService.updateLocation(location))
            {
                return R.OK();
            };
            return R.Error("更新失败，请检查提交的数据");
        }
        /// <summary>
        /// updateStatus
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        public R updateLocationStatus(WipStorageLocation location)
        {
            if (wipStorageService.updateLocationStatus(location))
            {
                return R.OK();
            }
            return R.Error("修改状态失败，请联系管理员");
        }
        /// <summary>
        ///分页查询
        ///pageNum: 1,
        ///pageSize: 10,
        ///keyword: null
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public R fetchLocationList(int pageNum, int pageSize, string keyword)
        {
            int total = 0;
            var locationlist = wipStorageService.fetchLocationList(pageNum, pageSize, keyword, ref total);
            return R.OK(locationlist).data("total", total);
        }
        #endregion

        #region 绑定api
        #region car&magazine info :info代表关联信息
        /// <summary>
        /// 验证车是否可以绑定
        /// </summary>
        /// <param name="carNo"></param>
        /// <returns></returns>
        [HttpGet]
        public R checkCarIsFullByCarNo(string carNo)
        {
            if (string.IsNullOrEmpty(carNo))
            {
                return R.Error("提交数据为空，请检查！");
            }
            if (!(wipStorageService.fetchWipCarByNo(carNo) == 1))
            {
                return R.Error("非法的小车信息，请扫入正确的小车二维码信息");
            }
            bool isFull = false;
            List<WipMagazineCarInfo> carInfos = wipStorageService.checkCarIsFullByCarNo(carNo, ref isFull);
            return isFull ? R.Error("该车已经绑定满了，不允许绑定！请注意") : R.OK(carInfos).message("验证通过！");

        }
        /// <summary>
        /// 验证magazine是否可以绑定
        /// </summary>
        /// <param name="magazineNo"></param>
        /// <returns></returns>
        [HttpGet]
        public R checkMagazineIsValidByNo(string magazineNo)
        {
            if (string.IsNullOrEmpty(magazineNo))
            {
                return R.Error("提交数据为空，请检查！");
            }
            int total = 0;
            // todo 需要精准查询重新写
            wipStorageService.fetchMagazineList(1, 1, magazineNo, ref total);
            if (!(total >= 1))
            {
                return R.Error("非法的Magazine信息，请扫入正确的Magazine二维码信息");
            }
            string carNo = string.Empty;
            return wipStorageService.checkMagazineIsValidByNo(magazineNo, ref carNo) ? R.Error(@"该Magazine已绑定到" + carNo + "，请注意！") : R.OK().message("验证通过！");

        }
        /// <summary>
        /// 保存绑定数据 -magazine与car
        /// </summary>
        /// <param name="wipMagazineCarInfos"></param>
        /// <returns></returns>
        [HttpPost]
        public R saveCarWithMagazineInfo(List<WipMagazineCarInfo> wipMagazineCarInfos)
        {
            if (wipMagazineCarInfos.Count < 0)
            {
                return R.Error("提交数据为空，请检查！");
            }
            if (!wipStorageService.saveCarWithMagazineInfo(wipMagazineCarInfos))
            {
                return R.Error("系统错误，找it");
            }
            return R.OK().message("提交成功！");

        }
        #endregion

        #region location & car --info
        /// <summary>
        /// 验证location是否可用
        /// </summary>
        /// <param name="locationNo"></param>
        /// <returns></returns>
        [HttpGet]
        public R checkLoctionIsValidByNo(string locationNo)
        {
            if (string.IsNullOrEmpty(locationNo))
            {
                return R.Error("提交数据为空，请检查！");
            }
            if (!(wipStorageService.fetchLoctionByNo(locationNo) == 1))
            {
                return R.Error("非法的库位信息，请扫入正确的库位二维码信息");
            }
            string carNo = string.Empty;
            return wipStorageService.checkLoctionIsValidByNo(locationNo,ref carNo) ? R.Error(@"该"+locationNo+"库位已经绑定车了，不允许绑定！请注意") : R.OK().message("验证通过！");

        }
        /// <summary>
        /// 验证车是否绑到其他的库位了
        /// </summary>
        /// <param name="carNo"></param>
        /// <returns></returns>
        [HttpGet]
        public R checkFullMagazineCarIsValidByNo(string carNo)
        {
            if (string.IsNullOrEmpty(carNo))
            {
                return R.Error("提交数据为空，请检查！");
            }
            if (!(wipStorageService.fetchWipCarByNo(carNo) == 1))
            {
                return R.Error("非法的小车信息，请扫入正确的小车二维码信息");
            }
            // 检查这辆车是否是空车，空车就不许绑定
            bool isFull = false;
            List<WipMagazineCarInfo> wipMagazineCarInfos = wipStorageService.checkCarIsFullByCarNo(carNo, ref isFull);
            if (!isFull && wipMagazineCarInfos.Count == 0)
            {
                return R.Error("该小车是空车，请注意，不允许绑定！");
            }
            string locationNo = string.Empty;
            return wipStorageService.checkFullMagazineCarIsValidByNo(carNo, ref locationNo) ? R.Error(@"该小车已绑定到" + locationNo + "，请注意！") : R.OK().message("验证通过！");

        }
        /// <summary>
        /// 保存car location的关系
        /// </summary>
        /// <param name="wipCarLocationInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public R saveLocationWithCarInfo(WipCarLocationInfo wipCarLocationInfo)
        {
            if (string.IsNullOrEmpty(wipCarLocationInfo.WipCarNo))
            {
                return R.Error("提交数据为空，请检查！");
            }
            if (!wipStorageService.saveLocationWithCarInfo(wipCarLocationInfo))
            {
                return R.Error("系统错误，找it");
            }
            return R.OK().message("提交成功！");

        }
        #endregion
        #endregion

        #region 解绑api
        #region location&car 
        /// <summary>
        /// 解绑验证locationNo是否可以
        /// </summary>
        /// <param name="locationNo"></param>
        /// <returns></returns>
        [HttpGet]
        public R checkUnbindLocationIsValidByNo(string locationNo)
        {

            if (string.IsNullOrEmpty(locationNo))
            {
                return R.Error("提交数据为空，请检查！");
            }
            if (!(wipStorageService.fetchLoctionByNo(locationNo) == 1))
            {
                return R.Error("非法的库位信息，请扫入正确的库位二维码信息");
            }
            string msg = string.Empty;
            return wipStorageService.checkUnbindLocationIsValidByNo(locationNo, ref msg) ? R.OK().message(message: "验证通过！"): R.Error(msg);

        }
        /// <summary>
        /// 保存car location的关系
        /// </summary>
        /// <param name="wipCarLocationInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public R unbindLocationWithCarInfo(WipCarLocationInfo wipCarLocationInfo)
        {
            if (string.IsNullOrEmpty(wipCarLocationInfo.LocationNo))
            {
                return R.Error("提交数据为空，请检查！");
            }
            if (!wipStorageService.unbindLocationWithCarInfo(wipCarLocationInfo))
            {
                return R.Error("系统错误，找it");
            }
            return R.OK().message("解绑成功！");

        }
        #endregion

        #endregion
    }
}
