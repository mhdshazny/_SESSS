using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Smart_Electrician_Support_System.Models;
using Smart_Electrician_Support_System.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_Electrician_Support_System.Services
{
    public class UsedProductsService
    {

        private static DbConnectionClass _context;
        private static IMapper _mapper;
        private readonly ProductsService _prdService;

        public UsedProductsService(DbConnectionClass context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _prdService = new ProductsService(context, mapper);
        }

        public static List<UsedProductsViewModel> GetList()
        {

            var DataList = _context.UsedProductsData.ToList();
            var GetList = new List<UsedProductsViewModel>();
            foreach (var item in DataList)
            {
                var ProductData = _context.ProductsData.Where(i => i.PrID == item.PrID).FirstOrDefault();

                var VM = _mapper.Map<UsedProductsViewModel>(item);
                VM.PrName = ProductData.PrName;
                VM.TotCost = "Rs." + (ProductData.PrPrice * item.PrQty).ToString("0.00");
                GetList.Add(VM);
            }
            return GetList;
        }

        public static async Task<bool> Add(UsedProductsViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var StockData = ProductsService.Find(collection.PrID);
                    bool LimitCheck = QtyLimitCheck(collection);
                    bool stockUpdated = false;

                    if (LimitCheck)
                    {
                        UsedProductsModel Final = _mapper.Map<UsedProductsModel>(collection);
                        stockUpdated = await DeductFromStockAsync(Final);
                    }
                    /////////////   
                    if (stockUpdated)
                    {

                        var MapData = _mapper.Map<UsedProductsModel>(collection);
                        _context.Add(MapData);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                    return false;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }

        internal static IEnumerable PrdList()
        {
            throw new NotImplementedException();
        }

        public static async Task<bool> Update(UsedProductsViewModel collection)
        {
            try
            {
                if (collection != null)
                {
                    var oldData = Find(collection.Pr_Used_ID);
                    bool stockUpdated = true;

                    if (collection.PrQty>oldData.PrQty)
                    {
                        int extraQty = collection.PrQty - oldData.PrQty;
                        UsedProductsModel Final = _mapper.Map<UsedProductsModel>(collection);
                        Final.PrQty = extraQty;

                        stockUpdated = await DeductFromStockAsync(Final);
                    }
                    else if (collection.PrQty<oldData.PrQty)
                    {
                        int extraQty = oldData.PrQty - collection.PrQty;
                        UsedProductsModel Final = _mapper.Map<UsedProductsModel>(collection);
                        Final.PrQty = extraQty;

                        stockUpdated = await ReturnToStockAsync(Final);
                    }
                    if (stockUpdated)
                    {
                        var MapData = _mapper.Map<UsedProductsModel>(collection);
                        _context.Update(MapData);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            catch (Exception err)
            {
                err.ToString();
                return false;
            }
        }

        internal static bool QtyLimitCheck(UsedProductsViewModel collection)
        {
            var ProductInfo = ProductsService.Find(collection.PrID);
            if (ProductInfo.PrQty >= collection.PrQty)
            {
                return true;
            }
            else
                return false;
        }

        internal static List<UsedProductsViewModel> GetListForElectrician(string id)
        {
            var OutData = new List<UsedProductsViewModel>();

            var JobData = _context.JobData.Where(i => i.Emp_Electr_ID == id).ToList();

            foreach (var item in JobData)
            {
                var DataList = _context.UsedProductsData.Where(i => i.Job_ID == item.Job_ID).ToList();
                if (DataList.Count>0)
                {
                    foreach (var usedProd in DataList)
                    {
                        var ProductData = _context.ProductsData.Where(i => i.PrID == usedProd.PrID).FirstOrDefault();

                        var mappedData = _mapper.Map<UsedProductsViewModel>(usedProd);
                        mappedData.PrName = ProductData.PrName;
                        mappedData.TotCost = "Rs." + (ProductData.PrPrice * usedProd.PrQty).ToString("0.00");
                        OutData.Add(mappedData);
                    }

                }
            }

            return OutData;
        }

        internal static List<UsedProductsViewModel> GetListByJid(string id)
        {
            var DataList = _context.UsedProductsData.Where(i=>i.Job_ID==id).ToList();
            var GetList = new List<UsedProductsViewModel>();
            foreach (var item in DataList)
            {
                var ProductData = _context.ProductsData.Where(i => i.PrID == item.PrID).FirstOrDefault();

                var VM = _mapper.Map<UsedProductsViewModel>(item);
                VM.PrName = ProductData.PrName;
                VM.TotCost = "Rs." + (ProductData.PrPrice * item.PrQty).ToString("0.00");
                GetList.Add(VM);
            }
            return GetList;
        }

        internal static string NewID()
        {
            var Id = _context.UsedProductsData
                .Max(i => i.Pr_Used_ID);
            string NewID = "UPD0000001";
            int num;
            if (Id != null)
            {
                num = int.Parse(Id.Substring(3, 7)) + 1;
                NewID = "UPD" + num.ToString().PadLeft(7, '0');
                return NewID;
            }
            return NewID;
        }

        internal static async Task<bool> Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    UsedProductsModel FoundRecord = _context.UsedProductsData.Find(id);
                    bool stockUpdate = await ReturnToStockAsync(FoundRecord);
                    if (stockUpdate)
                    {
                        _context.Remove(FoundRecord);
                        await _context.SaveChangesAsync();
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static UsedProductsViewModel Find(string id)
        {

            var FoundRecord = _context.UsedProductsData.Find(id);
            var ProductData = _context.ProductsData.Where(i => i.PrID == FoundRecord.PrID).FirstOrDefault();

            var VM = _mapper.Map<UsedProductsViewModel>(FoundRecord);
            VM.PrName = ProductData.PrName;
            VM.TotCost = "Rs."+(ProductData.PrPrice * FoundRecord.PrQty).ToString("0.00");
            return VM;
        }


        /// <summary>
        /// //////////
        /// </summary>

        internal static async Task<bool> DeductFromStockAsync(UsedProductsModel FoundRecord)
        {
            try
            {
                if (FoundRecord != null)
                {

                    ProductsViewModel productToUpdate = ProductsService.Find(FoundRecord.PrID);

                    productToUpdate.PrQty = productToUpdate.PrQty - FoundRecord.PrQty;

                    ProductsModel Final = _mapper.Map<ProductsModel>(productToUpdate);



                    bool updation = await ProductsService.Update(productToUpdate);
                    //_context.Update(Final);
                    //await _context.SaveChangesAsync();
                    return updation;
                }
                else
                    return false;
            }
            catch (Exception er)
            {

                return false;
            }
        }

        internal static async Task<bool> ReturnToStockAsync(UsedProductsModel FoundRecord)
        {
            try
            {
                if (FoundRecord != null)
                {
                    
                    ProductsModel productToUpdate = await _context.ProductsData.Where(i=>i.PrID==FoundRecord.PrID).FirstOrDefaultAsync();

                    productToUpdate.PrQty = productToUpdate.PrQty + FoundRecord.PrQty;

                    ProductsModel Final = _mapper.Map<ProductsModel>(productToUpdate);

                    ProductsViewModel VM = _mapper.Map<ProductsViewModel>(productToUpdate);


                    bool updation = await ProductsService.Update(VM);
                    //_context.Update(Final);
                    //await _context.SaveChangesAsync();
                    return updation;
                }
                else
                    return false;
            }
            catch (Exception er)
            {

                return false;
            }
        }




    }
}
