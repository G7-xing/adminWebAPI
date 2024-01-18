using EWI_System.Model.Enties;
using EWI_System.Model.Enties.WipStroage;

using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EWI_System.Service
{
    public class WipStorageService : IWipStorageService
    {
        #region 注册数据库连接，选择不同的数据库请在构造器中 dbconn--默认系统主数据库
        //添加dbconn.AsTenant().GetConnectionScope("ACE_Traceability_DB"); // ACE_Traceability_DB库
        private readonly ISqlSugarClient dbconn;
        private readonly ISqlSugarClient aceDB;
        public WipStorageService(ISqlSugarClient dbconn, ISqlSugarClient aceDB)
        {
            this.dbconn = dbconn;
            this.aceDB = aceDB.AsTenant().GetConnectionScope("ACE_Traceability_DB");
        }
        #endregion

        #region fectchLocationArea
        public List<WipLocationArea> fectchLocationArea()
        {
            #region 电脑个人证书的代码
            //X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            //store.Open(OpenFlags.MaxAllowed);
            //X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;//获取储存区上的所有证书
            //var sd = collection.Find((X509FindType)5, "350000fdaa731447efce0e2aeb00000000fdaa", true);
            ////X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindByIssuerName, "aliyun", false);//找到所有aliyun颁发的证书
            //X509Certificate2Collection fcollection = (X509Certificate2Collection)collection;//找到所有本地的个人证书
            #endregion
            return dbconn.Queryable<WipLocationArea>().Where(a => a.Deleted == 0).OrderBy(a => a.Sort).ToList();

        }
        #endregion

        #region magazine
        public List<TbMagazine> fetchMagazineList(int pageNum, int pageSize, string keyword, ref int totalCount)
        {
            return aceDB.Queryable<TbMagazine>().WhereIF(!String.IsNullOrEmpty(keyword), it => it.MagazineNo.Contains(keyword))
                                                 .OrderBy(W => W.MagazineNo)
                                                 .ToPageList(pageNum, pageSize, ref totalCount);
        }
        public List<TbMagazine> getMagazineByNo(string magazineNo)
        {
            return aceDB.Queryable<TbMagazine>().Where(t => t.MagazineNo == magazineNo).ToList();
        }
        #endregion

        #region wipCar
        /// <summary>
        /// add
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool createCar(WipCar wipCar)
        {
            return dbconn.Insertable(wipCar).ExecuteCommand() == 1;
        }
        /// <summary>
        /// batchAdd
        /// </summary>
        /// <param name="carList"></param>
        /// <returns></returns>
        public bool createBatchCar(List<WipCar> carList)
        {
            return dbconn.Insertable<WipCar>(carList).ExecuteCommand() == carList.Count;
        }
        /// <summary>
        /// fetchWipCarByNo
        /// </summary>
        /// <param name="wipCarNo"></param>
        /// <returns></returns>
        public int fetchWipCarByNo(string wipCarNo)
        {
            return dbconn.Queryable<WipCar>().Where(W => W.wipCarNo == wipCarNo).ToList().Count;
        }
        /// <summary>
        /// getLimteData
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<WipCar> fetchCarList(int pageNum, int pageSize, string keyword, ref int totalCount)
        {
            return dbconn.Queryable<WipCar>().WhereIF(!String.IsNullOrEmpty(keyword), it => it.wipCarNo.Contains(keyword))
                                             .LeftJoin<User>((W, U) => W.createBy == U.Id.ToString())
                                             .LeftJoin<User>((W, U, U1) => W.updateBy == U1.Id.ToString())
                                             .Select((W, U, U1) => new WipCar
                                             {
                                                 wipCarId = W.wipCarId,
                                                 wipCarNo = W.wipCarNo,
                                                 sort = W.sort,
                                                 status = W.status,
                                                 createTime = W.createTime,
                                                 createBy = U.UserName,
                                                 updateTime = W.updateTime,
                                                 updateBy = U1.UserName
                                             })
                                             .OrderBy(W => W.wipCarNo)
                                             .ToPageList(pageNum, pageSize, ref totalCount);

        }
        /// <summary>
        /// updateStatus
        /// </summary>
        /// <param name="WipCar"></param>
        /// <returns></returns>
        public bool updateCarStatus(WipCar wipCar)
        {
            return dbconn.Updateable<WipCar>().SetColumns(r => new WipCar { status = wipCar.status, updateBy = wipCar.updateBy })
                                              .Where(r => r.wipCarId == wipCar.wipCarId).ExecuteCommandHasChange();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="wipCar"></param>
        /// <returns></returns>
        public bool updateCar(WipCar wipCar)
        {
            return dbconn.Updateable<WipCar>().SetColumns(r => new WipCar { wipCarNo = wipCar.wipCarNo, status = wipCar.status, updateBy = wipCar.updateBy })
                                              .Where(r => r.wipCarId == wipCar.wipCarId).ExecuteCommandHasChange();
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="wipCarId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool deleteCar(int wipCarId, out string msg)
        {
            msg = string.Empty;
            if (!dbconn.Deleteable<WipCar>().Where(it => it.wipCarId == wipCarId).ExecuteCommandHasChange())
            {
                msg = "执行语句有异常";
                return false;
            }
            return true;
        }
        #endregion

        #region location
        /// <summary>
        /// add
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool createLocation(WipStorageLocation location)
        {
            return dbconn.Insertable(location).ExecuteCommand() == 1;
        }

        /// <summary>
        /// batchAdd
        /// </summary>
        /// <param name="locationList"></param>
        /// <returns></returns>
        public bool createBatchLocation(List<WipStorageLocation> locationList)
        {
            return dbconn.Insertable<WipStorageLocation>(locationList).ExecuteCommand() == locationList.Count;
        }

        /// <summary>
        /// fetchLoctionByNo
        /// </summary>
        /// <param name="loctionNo"></param>
        /// <returns></returns>
        public int fetchLoctionByNo(string loctionNo)
        {
            return dbconn.Queryable<WipStorageLocation>().Where(W => W.LocationNo == loctionNo).ToList().Count;
        }
        /// <summary>
        /// getLimteData
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<WipStorageLocation> fetchLocationList(int pageNum, int pageSize, string keyword, ref int totalCount)
        {
            return dbconn.Queryable<WipStorageLocation>().WhereIF(!String.IsNullOrEmpty(keyword), it => it.LocationNo.Contains(keyword))
                                             .LeftJoin<User>((W, U) => W.CreateBy == U.Id.ToString())
                                             .LeftJoin<User>((W, U, U1) => W.UpdateBy == U1.Id.ToString())
                                             .LeftJoin<WipLocationArea>((W, U, U1, A) => W.WipLocationAreaId == A.WipLocationAreaId)
                                             .Select((W, U, U1, A) => new WipStorageLocation
                                             {
                                                 WipStorageLocationId = W.WipStorageLocationId,
                                                 LocationNo = W.LocationNo,
                                                 Sort = W.Sort,
                                                 Status = W.Status,
                                                 CreateTime = W.CreateTime,
                                                 CreateBy = U.UserName,
                                                 UpdateTime = W.UpdateTime,
                                                 UpdateBy = U1.UserName,
                                                 WipLocationAreaId = W.WipLocationAreaId,
                                                 AreaLineName = A.AreaLineName,
                                                 AreaName = A.AreaName
                                             })
                                             .OrderBy(W => W.LocationNo)
                                             .ToPageList(pageNum, pageSize, ref totalCount);

        }
        /// <summary>
        /// updateStatus
        /// </summary>
        /// <param name="WipStorageLocation"></param>
        /// <returns></returns>
        public bool updateLocationStatus(WipStorageLocation location)
        {
            return dbconn.Updateable<WipStorageLocation>().SetColumns(r => new WipStorageLocation { Status = location.Status, UpdateBy = location.UpdateBy })
                                              .Where(r => r.WipStorageLocationId == location.WipStorageLocationId).ExecuteCommandHasChange();
        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool updateLocation(WipStorageLocation location)
        {
            return dbconn.Updateable<WipStorageLocation>().SetColumns(r => new WipStorageLocation { LocationNo = location.LocationNo, WipLocationAreaId = location.WipLocationAreaId, Status = location.Status, UpdateBy = location.UpdateBy })
                                              .Where(r => r.WipStorageLocationId == location.WipStorageLocationId).ExecuteCommandHasChange();
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool deleteLocation(int locationId, out string msg)
        {
            msg = string.Empty;
            if (!dbconn.Deleteable<WipStorageLocation>().Where(it => it.WipStorageLocationId == locationId).ExecuteCommandHasChange())
            {
                msg = "执行语句有异常";
                return false;
            }
            return true;
        }

        #endregion


        #region car&magazine info :info代表关联信息 严格控制delete=0的赋值，这是确保历史的关键

        public List<WipMagazineCarInfo> checkCarIsFullByCarNo(string carNo, ref bool isFull)
        {
            // TODO: 处理验证三种情况
            List<WipMagazineCarInfo> carInfo = dbconn.Queryable<WipMagazineCarInfo>().Where(W => W.WipCarNo == carNo)
                                                                                     .Where(W => W.Deleted == 0)
                                                                                     .OrderBy(W => W.CreateTime).ToList();
            //1. 全新的car，没有和magazine绑定
            //2. 有magazine绑定，但是不满4个的；
            //3. 满了4个magazine，不允许绑定==>走解绑流程；
            _ = carInfo.Count == 4 ? isFull = true : isFull = false;
            return carInfo;
        }

        public bool checkMagazineIsValidByNo(string magazineNo, ref string carNo)
        {
            // TODO：验证magazine是否能绑定小车上
            // 1. 查可有这个no存在且是delete=0的数据,magazine在此表中必须有且仅有一条
            List<WipMagazineCarInfo> magazineInfos = dbconn.Queryable<WipMagazineCarInfo>().Where(W => W.MagazineNo == magazineNo)
                                                                                           .Where(W => W.Deleted == 0).ToList();
            _ = magazineInfos.Count == 1 ? carNo = magazineInfos[0].WipCarNo : carNo = null;
            return magazineInfos.Count == 1;
        }

        public bool saveCarWithMagazineInfo(List<WipMagazineCarInfo> wipMagazineCarInfos)
        {
            // TODO: 保存小车与magazine的绑定数据 事务
            var result = dbconn.AsTenant().UseTran(() =>
            {
                // 先插入magazine--car--info
                // var magazineCarInfoIds = dbconn.Insertable<WipMagazineCarInfo>(wipMagazineCarInfos).ExecuteReturnPkList<Guid>();
                //// 将返回的magazineCarInfoIds插入到 wipInfoRelation
                //List<WipInfoRelation> wipInfoRelations = new List<WipInfoRelation>();
                //magazineCarInfoIds.ForEach(magazineCarInfoId =>
                //{
                //    WipInfoRelation infoRelation = new WipInfoRelation
                //    {
                //        WipMagazineCarInfoId = magazineCarInfoId.ToString().ToUpper(),
                //        CreateBy = wipMagazineCarInfos[0].CreateBy,
                //        UpdateBy = wipMagazineCarInfos[0].UpdateBy
                //    };
                //    wipInfoRelations.Add(infoRelation);
                //});
                return dbconn.Insertable<WipMagazineCarInfo>(wipMagazineCarInfos).ExecuteCommand() == wipMagazineCarInfos.Count();
            });
            return result.Data;


        }
        #endregion

        #region location & car --info代表关联信息 严格控制delete=0的赋值，这是确保历史的关键
        public bool checkLoctionIsValidByNo(string locationNo, ref string carNo)
        {
            WipCarLocationInfo wipCarLocationInfo = dbconn.Queryable<WipCarLocationInfo>().Where(W => W.LocationNo == locationNo)
                                                         .Where(W => W.Deleted == 0).First();
            if (wipCarLocationInfo == null)
            {
                return false;
            }
            else
            {
                carNo = wipCarLocationInfo.WipCarNo;
                return true;
            }

        }

        public bool checkFullMagazineCarIsValidByNo(string carNo, ref string locationNo)
        {

            // 检查关系库
            WipCarLocationInfo wipCarLocationInfo = dbconn.Queryable<WipCarLocationInfo>().Where(W => W.WipCarNo == carNo)
                                                         .Where(W => W.Deleted == 0).First();
            if (wipCarLocationInfo == null)
            {
                return false;
            }
            else
            {
                locationNo = wipCarLocationInfo.LocationNo;
                return true;
            }
        }

        public bool saveLocationWithCarInfo(WipCarLocationInfo wipCarLocationInfo)
        {
            var result = dbconn.AsTenant().UseTran(() =>
            {
                List<Guid> wipCarLocationInfoId = dbconn.Insertable(wipCarLocationInfo).ExecuteReturnPkList<Guid>();
                List<Guid> wipMagazineCarInfoIds = dbconn.Queryable<WipMagazineCarInfo>().Where(w => w.WipCarNo == wipCarLocationInfo.WipCarNo)
                                                                                         .Where(w => w.Deleted == 0)
                                                                                         .Select(w => w.WipMagazineCarInfoId).ToList();
                List<WipInfoRelation> infoRelations = new List<WipInfoRelation>();
                foreach (var item in wipMagazineCarInfoIds)
                {
                    WipInfoRelation infoRelation = new WipInfoRelation
                    {
                        WipCarLocationInfoId = wipCarLocationInfoId[0].ToString().ToUpper(),
                        WipMagazineCarInfoId = item.ToString().ToUpper(),
                        UpdateBy = wipCarLocationInfo.UpdateBy,
                        CreateBy = wipCarLocationInfo.CreateBy
                    };
                    infoRelations.Add(infoRelation);
                }
                return dbconn.Insertable<WipInfoRelation>(infoRelations).ExecuteCommand() == infoRelations.Count;
            });
            return result.Data;
        }
        //.Where(w => DateTime.Parse(w.UpdateBy).AddMinutes(30)>=DateTime.Now).ToList();
        public bool checkUnbindLocationIsValidByNo(string locationNo, ref string msg)
        {
            WipCarLocationInfo carLocationInfo = dbconn.Queryable<WipCarLocationInfo>().Where(w => w.LocationNo == locationNo)
                                                                          .Where(w => w.Deleted == 0).First();
            if (carLocationInfo == null)
            {
                // 就要去检查是否在半小时内多次绑定
                List<WipCarLocationInfo> carLocationInfoDeleteds = dbconn.Queryable<WipCarLocationInfo>().Where(w => w.LocationNo == locationNo)
                                                                                                       .Where(w => w.Deleted == 1)
                                                                                                       .Where(w => DateTime.Parse(w.UpdateTime).AddMinutes(10) >= DateTime.Now)
                                                                                                       .ToList();
                if (carLocationInfoDeleteds.Count >= 2)
                {
                    var wipCarNos = carLocationInfoDeleteds.GroupBy(item => item.WipCarNo)
                                            .Select(group => new { carNo = group.Key, Count = group.Count() })
                                            .ToList().OrderByDescending(x => x.Count).FirstOrDefault();
                    if (wipCarNos != null && wipCarNos.Count >= 2)
                    {
                        msg = @"该" + locationNo + "库位上的小车" + wipCarNos.carNo + ",在十分钟内多次重复出入库，此违规操作，请注意！";
                        return false;
                    }
                }
                msg = "该库位上没有绑定小车，不允许解绑，请注意！";
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool unbindLocationWithCarInfo(WipCarLocationInfo wipCarLocationInfo)
        {
            // 找到这个carNo，得到id，在relation中去delete==1
            var result = dbconn.AsTenant().UseTran(() =>
            {
                WipCarLocationInfo carLocationInfo = dbconn.Queryable<WipCarLocationInfo>().Where(w => w.LocationNo == wipCarLocationInfo.LocationNo)
                                                                          .Where(w => w.Deleted == 0).First();
                int relations = dbconn.Queryable<WipInfoRelation>().Where(W => W.WipCarLocationInfoId == carLocationInfo.WipCarLocationInfoId.ToString()).Count();
                // 封装联系表的数据
                WipInfoRelation wipInfoRelation = new WipInfoRelation()
                {
                    UpdateBy = wipCarLocationInfo.UpdateBy,
                    UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Deleted = 1
                };
                // 封装car&location的数据
                wipCarLocationInfo.Deleted = 1;
                wipCarLocationInfo.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var sd = dbconn.Updateable(wipCarLocationInfo)
                                                 .UpdateColumns(W => new { W.UpdateBy, W.UpdateTime, W.Deleted })
                                                 .Where(W => W.WipCarLocationInfoId == carLocationInfo.WipCarLocationInfoId)
                                                 .Where(W => W.Deleted == 0)
                                                 .ExecuteCommand() == 1;
                var ssd = dbconn.Updateable<WipInfoRelation>(wipInfoRelation)
                                                .UpdateColumns(W => new { W.UpdateBy, W.UpdateTime, W.Deleted })
                                                .Where(W => W.WipCarLocationInfoId == carLocationInfo.WipCarLocationInfoId.ToString())
                                                .Where(W => W.Deleted == 0).ExecuteCommand() == relations;
                return ssd && sd;
                ;
            });
            return result.Data;
        }

        public List<PcbaData> getInStorageDataByPcbaNo(string pcbaNo)
        {
            string sql = string.Format(@"select xx.magazineNo,xx.WipCarNo,xx.LocationNo,xx.AreaName,CONVERT(varchar(16), min(xx.uploaddate), 120) minDate from (
                                                    select a.WipCarNo,a.LocationNo ,y.AreaName ,c.MagazineNo ,d.pcbaPartNo,d.uploaddate from WipCarLocationInfo a 
                                                    right join WipStorageLocation x on x.LocationNo = a.LocationNo 
                                                    right join WipLocationArea y on x.WipLocationAreaId  = y.WipLocationAreaId 
                                                    right join WipInfoRelation b on a.WipCarLocationInfoId  = b.WipCarLocationInfoId 
                                                    right join WipMagazineCarInfo c on b.WipMagazineCarInfoId = c.WipMagazineCarInfoId 
                                                    right join [10.124.12.33].[ACE_Traceability].[dbo].[Tb_WIP_InOut] d  on d.magazineNo  collate Chinese_PRC_CI_AS = c.MagazineNo collate Chinese_PRC_CI_AS 
                                                    where a.Deleted = 0 and d.pcbaPartNo = '{0}' 
                                                ) xx group by xx.magazineNo,xx.WipCarNo,xx.LocationNo,xx.AreaName", pcbaNo);
            List<PcbaData> pcbaDatas = dbconn.Ado.SqlQuery<PcbaData>(sql);
            return pcbaDatas.OrderBy(i => i.minDate).ToList();
        }

        public bool getUnbindCarNoOfLocation(string carNo, ref string locationNo)
        {
            WipCarLocationInfo carLocationInfo = dbconn.Queryable<WipCarLocationInfo>().Where(w => w.WipCarNo == carNo)
                                                                          .Where(w => w.Deleted == 0).First();
            if (carLocationInfo == null)
            {
                return false;
            }
            else
            {
                locationNo = carLocationInfo.LocationNo;
                return true;
            }
        }


        #endregion
    }
}
