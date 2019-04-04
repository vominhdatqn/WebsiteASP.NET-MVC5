using QuanLyXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class ShoppingCart
{
    public static ShoppingCart Cart
    {
        get
        {
            var cart = HttpContext.Current.Session["GioHang"] as ShoppingCart;
            // Nếu chưa có giỏ hàng trong session -> tạo mới và lưu vào session
            if (cart == null)
            {
                cart = new ShoppingCart();
                HttpContext.Current.Session["GioHang"] = cart;
            }
            return cart;
        }
    }
    public List<SanPham> Items = new List<SanPham>();
    public void Add(int id)
    {
        try // tìm thấy trong giỏ -> tăng số lượng lên 1
        {
            var item = Items.Single(i => i.MaSP == id);
            item.SoLuongTon++;
        }
        catch // chưa có trong giỏ -> truy vấn CSDL và bỏ vào giỏ
        {
            var db = new QLBanXeEntities3();
            var item = db.SanPhams.Find(id);
            item.SoLuongTon = 1;
            Items.Add(item);
        }
    }

    public void Remove(int id)
    {
        var item = Items.Single(i => i.MaSP == id);
        Items.Remove(item);
    }

    public void Update(int id, int newQuantity)
    {
        var item = Items.Single(i => i.MaSP == id);
        item.SoLuongTon = newQuantity;
    }

    public void Clear()
    {
        Items.Clear();
    }

    public int Count
    {
        get
        {
            return Items.Count;
        }
    }

    public double Total
    {
        get
        {
            return Items.Sum(n => n.GiaBan * n.SoLuongTon);

        }
    }
}
//public class ShoppingCart
//{
//    public ShoppingCart()
//    {
//        ListItem = new List<ShoppingCartItem>();
//    }
//    public List<ShoppingCartItem> ListItem { get; set; }
//    public void AddToCart(ShoppingCartItem item)
//    {
//        if (ListItem.Where(s => s.iMaSP.Equals(item.iTenSP)).Any())
//        {
//            var myItem = ListItem.Single(s => s.iMaSP.Equals(item.iTenSP));
//            myItem.iSoLuong += item.iSoLuong;
//            myItem.Total += item.iSoLuong * Convert.ToDouble(item.iGiaBan.Trim().Replace(",", string.Empty).Replace(".", string.Empty));
//        }
//        else
//        {
//            ListItem.Add(item);
//        }
//    }
//    public bool RemoveFromCart(int lngProductSellID)
//    {
//        ShoppingCartItem existsItem = ListItem.Where(x => x.iMaSP == lngProductSellID).SingleOrDefault();
//        if (existsItem != null)
//        {
//            ListItem.Remove(existsItem);
//        }
//        return true;
//    }
//    public bool UpdateQuantity(int lngProductSellID, int intQuantity)
//    {
//        ShoppingCartItem existsItem = ListItem.Where(x => x.iMaSP == lngProductSellID).SingleOrDefault();
//        if (existsItem != null)
//        {
//            existsItem.iSoLuong = intQuantity;
//            existsItem.Total = existsItem.iSoLuong * Convert.ToDouble(existsItem.iGiaBan.Replace(",", string.Empty).Replace(".", string.Empty));
//        }
//        return true;
//    }
//    public bool EmptyCart()
//    {
//        ListItem.Clear();
//        return true;
//    }
//}
//public class ShoppingCartItem
//{
//    public int iMaSP { get; set; }
//    public string iTenSP { get; set; }
//    public string iGiaBan { get; set; }
//    public int iSoLuong { get; set; }
//    public double Total { get; set; }
//}

