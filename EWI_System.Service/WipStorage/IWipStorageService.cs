using EWI_System.Model.Enties;
using EWI_System.Model.Enties.WipStroage;

using System;
using System.Collections.Generic;
using System.Text;

namespace EWI_System.Service
{
    public interface IWipStorageService
    {
        List<WipLocationArea> fectchLocationArea();
        List<TbMagazine> fetchMagazineList(int pageNum, int pageSize, string keyword, ref int total);
        List<TbMagazine> getMagazineByNo(string magazineNo);

        #region wipcar
        bool createCar(WipCar wipCar);
        List<WipCar> fetchCarList(int pageNum, int pageSize, string keyword, ref int total);
        bool updateCarStatus(WipCar wipCar);
        bool updateCar(WipCar wipCar);
        bool deleteCar(int wipCarId, out string msg);
        int fetchWipCarByNo(string wipCarNo);
        bool createBatchLocation(List<WipStorageLocation> locationList);
        #endregion

        #region wipStorageLocation
        bool createLocation(WipStorageLocation location);
        List<WipStorageLocation> fetchLocationList(int pageNum, int pageSize, string keyword, ref int total);
        bool deleteLocation(int wipStorageLocationId, out string msg);
        bool updateLocation(WipStorageLocation location);
        bool updateLocationStatus(WipStorageLocation location);
        int fetchLoctionByNo(string loctionNo);
        bool createBatchCar(List<WipCar> carList);
        #endregion

        #region car&magazine info :info代表关联信息
        List<WipMagazineCarInfo> checkCarIsFullByCarNo(string carNo,ref bool isFull);
        bool checkMagazineIsValidByNo(string magazineNo, ref string carNo);
        bool saveCarWithMagazineInfo(List<WipMagazineCarInfo> wipMagazineCarInfos);
        #endregion

        #region location & car --info
        bool checkLoctionIsValidByNo(string locationNo,ref string carNo);
        bool checkFullMagazineCarIsValidByNo(string carNo, ref string locationNo);
        bool saveLocationWithCarInfo(WipCarLocationInfo wipCarLocationInfo);
        bool checkUnbindLocationIsValidByNo(string locationNo, ref string msg);
        bool unbindLocationWithCarInfo(WipCarLocationInfo wipCarLocationInfo);
        List<PcbaData> getInStorageDataByPcbaNo(string pcbaNo);
       
        #endregion
    }
}
