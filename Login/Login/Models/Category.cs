using System;
using System.Collections.Generic;
using System.Text;
using Login.Views;
using SQLite;
namespace Login.Models
{
    public class CategoryListItem : Category
    {
        public int times_used { get; set; }
        public CategoryListItem(Category cat)
        {
            this.category_id = cat.category_id;
            this.category_name = cat.category_name;
            this.times_used = this.get_times_used();
        }
    }

    public class Category
    {  
        [PrimaryKey,AutoIncrement]
        public int category_id { set; get; }
        public string category_name { set; get; }

        public override string ToString()
        {
            return category_name;
        }

        static public List<Category> get_all_categories()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);

            return db.Table<Category>().OrderBy(c => c.category_name).ToList();
        }

        static public List<CategoryListItem> get_all_categories_list_items()
        {
            List<Category> cats = Category.get_all_categories();
            List<CategoryListItem> cats_list = new List<CategoryListItem>();
            foreach(Category cat in cats)
            {
                cats_list.Add(new CategoryListItem(cat));
            }
            return cats_list;
        }

        public int get_times_used()
        {
            SQLiteConnection db = new SQLiteConnection(App._dbPath);
            return db.Table<Printer>().Where(p => p.category_id == this.category_id).Count();
        }
    }
}
