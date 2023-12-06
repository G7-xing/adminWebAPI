using AutoMapper;

using EWI_System.Common;
using EWI_System.Model;
using EWI_System.Model.Enties;
using EWI_System.Service.MPSKanban.Dto;

using Microsoft.Extensions.Logging;

using NetTaste;

using SqlSugar;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EWI_System.Service
{
    public class MPSKanbanService : IMPSKanbanService
    {

        private readonly ISqlSugarClient dbAce;// 主要是ace的库
        private readonly ISqlSugarClient asmDB; // asm贴片机库
        private readonly IUserService userService;
        private readonly ILogger<MPSKanbanService> _logger;

        public MPSKanbanService(ISqlSugarClient dbAce, ISqlSugarClient asmDB, ILogger<MPSKanbanService> logger, IUserService userService)
        {
            this.dbAce = dbAce.AsTenant().GetConnectionScope("ACE_Traceability_DB"); // ACE_Traceability_DB库 
            this.asmDB = asmDB.AsTenant().GetConnectionScope("SiplaceSetupCenter_DB"); // SiplaceSetupCenter_DB库 贴片机的
            _logger = logger;
            this.userService = userService;
        }
        /// <summary>
        /// 带查询的分页
        /// </summary>
        /// <param name="pickOrderReq"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<vCfmPickOrders> fetchPickupList(PickupReq pickupReq, ref int total)
        {
            List<vCfmPickOrders> list = dbAce.Queryable<vCfmPickOrders>()
                .WhereIF(!String.IsNullOrEmpty(pickupReq.ComponentNumber), T => T.ComponentNumber == pickupReq.ComponentNumber.Trim().ToUpper())
                .WhereIF(!String.IsNullOrEmpty(pickupReq.UniqueId), T => T.UniqueId == pickupReq.UniqueId.Trim().ToUpper())
                .WhereIF(pickupReq.PickOrderType != 0, T => T.PickOrderType == pickupReq.PickOrderType)
                .WhereIF(pickupReq.PickupStatus != 0, T => T.PickupStatus == pickupReq.PickupStatus)
                .WhereIF(!String.IsNullOrEmpty(pickupReq.LineName), T => T.Line == pickupReq.LineName.Trim().ToUpper())
                .WhereIF(!String.IsNullOrEmpty(pickupReq.PickupTime), T => T.PickupDateTime >= DateTime.Parse(pickupReq.PickupTime))
                .Where(T => SqlFunc.Contains(T.PickOrderId, "CFM"))
                .OrderByDescending(T => T.PickOrderId)
                .ToPageList(pickupReq.PageNum, pickupReq.PageSize, ref total);
            return list;
        }
        /// <summary>
        /// 展开行的数据
        /// </summary>
        /// <param name="pickOrderId"></param>
        /// <returns></returns>
        public List<TbMpsPickorderdetails> expandRowList(string pickOrderId)
        {
            return dbAce.Queryable<TbMpsPickorderdetails>().Where(T => T.PickOrderId == pickOrderId)
                  .OrderBy(T => T.PartNumber).ToList();
        }

        /// <summary>
        /// 带查询的分页
        /// </summary>
        /// <param name="pickOrderReq"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<TbMpsPickorders> fetchHistoryList(PickOrderReq pickOrderReq, ref int total)
        {
            var list = dbAce.Queryable<TbMpsPickorders>()
                .WhereIF(!String.IsNullOrEmpty(pickOrderReq.PickOrderNumber), T => T.PickOrderId == pickOrderReq.PickOrderNumber.Trim().ToUpper())
                .WhereIF(!String.IsNullOrEmpty(pickOrderReq.PcbaPartNumber), T => T.PartNumber == pickOrderReq.PcbaPartNumber.Trim().ToUpper())
                .WhereIF(pickOrderReq.PickOrderType != 0, T => T.PickOrderType == pickOrderReq.PickOrderType)
                .WhereIF(pickOrderReq.PickOrderStatus != 0, T => T.PickStatus == pickOrderReq.PickOrderStatus)
                .WhereIF(!String.IsNullOrEmpty(pickOrderReq.LineName), T => T.Line == pickOrderReq.LineName.Trim().ToUpper())
                .WhereIF(!String.IsNullOrEmpty(pickOrderReq.CreatedTime), T => T.Created >= DateTime.Parse(pickOrderReq.CreatedTime))
                .OrderByDescending(T => T.PickOrderId)
                .ToPageList(pickOrderReq.PageNum, pickOrderReq.PageSize, ref total);
            return list;
        }
        /// <summary>
        /// kanban 的sql
        /// </summary>
        /// <returns></returns>
        public List<MPSKanbanRes> getDeatils()
        {
            string sql = @"select
								[PickOrderId],
								[PartNumber],
								[Line],
								[PickStatus],
								po.[WorkOrder],
								[PickOrderType],
								[Created],
								[Required],
								DATEDIFF(MI, [Created], getdate()) ExpiredMinutes
							from
								[Tb_MPS_PickOrders] po
							LEFT join [Tb_MPS_SmtSetup] ss on
								po.Line = ss.CfmSmtLineName
							where
								PickStatus = 10
								and PlantSite = 'CFM-E'
							order by
								PickOrderType DESC,
								PickStatus ASC,
								Created ASC";
            var list = dbAce.Ado.SqlQuery<MPSKanbanRes>(sql);

            return list;
        }
        /// <summary>
        /// asm分页查询带条件
        /// </summary>
        /// <param name="asmSetupReq"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<vPackagingUnitInSetup> fetchASMSetupList(AsmSetupReq asmSetupReq, ref int total)
        {
            var list = asmDB.Queryable<vPackagingUnitInSetup>()
               .WhereIF(!String.IsNullOrEmpty(asmSetupReq.AsmUID), T => T.PU1_PackagingUnitId == asmSetupReq.AsmUID || T.PU2_PackagingUnitId == asmSetupReq.AsmUID || T.PU3_PackagingUnitId == asmSetupReq.AsmUID)
               .WhereIF(!String.IsNullOrEmpty(asmSetupReq.AsmPartNumber), T => T.PU1_SiplaceProComponent == asmSetupReq.AsmPartNumber.Trim().ToUpper())
               .WhereIF(!String.IsNullOrEmpty(asmSetupReq.LineName), T => T.Line == asmSetupReq.LineName.Trim().ToUpper())
               //.OrderByDescending(T => T.)
               .ToPageList(asmSetupReq.PageNum, asmSetupReq.PageSize, ref total);
            foreach (var item in list)
            {
                item.TrackName = item.Station + "__" + item.Location + "_" + item.Track + "_" + item.Division;
            }
            return list;
        }

        #region SMTMPSPDA

        public List<TbMpsPickorders> fetchMPSofLineList(List<string> lineMPSRes)
        {
            return dbAce.Queryable<TbMpsPickorders>().In(t => t.Line, lineMPSRes)
                .Where(t => t.PickStatus == 10 && t.PickOrderId.Contains("CFM")).OrderBy(t => t.PickOrderId).ToList();
        }

        public List<TbMpsPickorderdetails> fetchMPSDetailById(string pickOrderId)
        {
            return dbAce.Queryable<TbMpsPickorderdetails>().Where(t => t.PickOrderId == pickOrderId).ToList();
        }
        /**
         * 要给外部接口传值
         */
        public bool handleQADAndMPS(MPSPDAReqcs mPSPDA,ref string errMsg)
        {
            // 依据userid找到工号
            User user = userService.getUserById(mPSPDA.userid);
            Task<string> qadResult = callSoapService(mPSPDA.uniqueId, user.NickName);
            // 解析返回的xml
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(qadResult.ToString());
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaceManager.AddNamespace("ns", "http://tempuri.org/");
            XmlNode canPassNode = xmlDoc.SelectSingleNode("//ns:canpass", namespaceManager);

            if (canPassNode.InnerText == "true") // 请求成功
            {
                // 更新detail 以及 Tb_MPS_PickOrders
                int? maxId = dbAce.Queryable<TbMpsPickorderdetails>()
                        .Where(s => s.UniqueId == null && s.PickOrderId == mPSPDA.pickOrderId && s.PartNumber == mPSPDA.rawPartNumber)
                        .Max(s => s.Id);
                var update = dbAce.Updateable<TbMpsPickorderdetails>().SetColumns(t => new TbMpsPickorderdetails { UniqueId = mPSPDA.uniqueId, PickStatus = 20 })
                                                        .Where(t => t.Id == maxId).ExecuteCommand();
                int checkIsFull = dbAce.Queryable<TbMpsPickorderdetails>().Where(j => j.UniqueId == null && j.PickOrderId == mPSPDA.pickOrderId).Count();
                if (checkIsFull == 0)
                {
                    dbAce.Updateable<TbMpsPickorders>().SetColumns(t => new TbMpsPickorders { PickStatus = 30 })
                                                        .Where(t => t.PickOrderId == mPSPDA.pickOrderId).ExecuteCommand();
                }
                // 插入 logs 表 --- 可以记录在log4net更好，但是你懂的哈哈哈哈
                dbAce.Insertable<TbMpsUseroperationlogs>(new TbMpsUseroperationlogs
                {
                    Pickorderid = mPSPDA.pickOrderId,
                    PartNo = mPSPDA.rawPartNumber,
                    UniqueId = mPSPDA.uniqueId,
                    Staffid = user.NickName,
                    QADuser = "ACED301",
                    Barcode = mPSPDA.barcode,
                    QADreturndata = qadResult.ToString(),
                    Mark = "succeed",
                    Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Type = "RCS"
                }).ExecuteCommand();
                return update == 1;
            }
            else // 请求失败
            {
                XmlNode errorMessageNode = xmlDoc.SelectSingleNode("//ns:errorMessage", namespaceManager);
                errMsg = errorMessageNode.InnerText;
                // 插入 logs 表
                dbAce.Insertable<TbMpsUseroperationlogs>(new TbMpsUseroperationlogs
                {
                    Pickorderid = mPSPDA.pickOrderId,
                    PartNo = mPSPDA.rawPartNumber,
                    UniqueId = mPSPDA.uniqueId,
                    Staffid = user.NickName,
                    QADuser = "ACED301",
                    Barcode = mPSPDA.barcode,
                    QADreturndata = qadResult.ToString(),
                    Mark = "Fail",
                    Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Type = "RCS"
                }).ExecuteCommand();
                return false;
            }

        }
        /// <summary>
        /// 请求QAD移库的SOAP接口
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <param name="nikcName"></param>
        /// <returns></returns>
        public async Task<string> callSoapService(string uniqueId, string nikcName)
        {
            var soapRequest = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                        <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                            xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
                            xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                            <soap:Body>
                                <CFMTranLoc xmlns=""http://tempuri.org/"">
                                    <input_tranloc>
                                        <intranloc>
                                            <intranloc_boxcode>{0}</intranloc_boxcode>
                                            <intranloc_qty>0</intranloc_qty>
                                            <intranloc_site>CSD01</intranloc_site>
                                            <intranloc_loc>PC01</intranloc_loc>
                                            <intranloc_user>{1}</intranloc_user>
                                        </intranloc>
                                    </input_tranloc>
                                </CFMTranLoc>
                            </soap:Body>
                        </soap:Envelope>", uniqueId, nikcName);

            using (var httpClient = new HttpClient())
            {
                var requestContent = new StringContent(soapRequest, Encoding.UTF8, "text/xml");

                // 设置请求头
                requestContent.Headers.Add("SOAPAction", "http://tempuri.org/CFMTranLoc");

                // 发送POST请求并获取响应
                var response = await httpClient.PostAsync("http://10.124.12.47:806/ESUN_WEB.asmx?op=CFMTranLoc", requestContent);

                // 读取响应内容
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
        }
        #endregion
    }
}
