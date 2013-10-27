using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Collections;
using UkrainianCurrency.Resources;


namespace UkrainianCurrency.Models
{
    public class Tables
    {
        public const int REGION = 0;
        public const int CITY = 1;

        public static Dictionary<Int32, String[]> CITIES = new Dictionary<Int32, String[]> 
            { 
                { 0, new String[] { "", "" } },
                { 1, new String[] { "Vinnytsia", "vinnista" } },
                { 2, new String[] { "Dnipropetrovsk", "Dnepropetrovsk" } },
                { 3, new String[] { "Donetsk", "donestk" } },
                { 4, new String[] { "Zhytomyr", "zhitomir" } },
                { 5, new String[] { "Zaporizhzhya", "zaporozhe" } },
                { 6, new String[] { "Ivano-Frankivsk", "ivano-frankovsk" } },
                { 7, new String[] { "Kyiv", "kiev" } },
                { 8, new String[] { "Kirovohrad", "kirovograd" } },
                { 9, new String[] { "Luhansk", "lugansk" } },
                { 10, new String[] { "Volyn", "lustk" } },
                { 11, new String[] { "Lviv", "lvov" } },
                { 12, new String[] { "Mykolaiv", "nikolaev" } },
                { 13, new String[] { "Odesa", "odessa" } },
                { 14, new String[] { "Poltavskaja", "poltava" } },
                { 15, new String[] { "Rivne", "rovno" } },
                { 16, new String[] { "Crimea", "simferopol" } },
                { 17, new String[] { "Sumy", "sumy" } },
                { 18, new String[] { "Tern", "ternopol" } },
                { 19, new String[] { "Carpathian", "uzhgorod" } },
                { 20, new String[] { "Kharkiv", "kharkov" } },
                { 21, new String[] { "Kherson", "kherson" } },
                { 22, new String[] { "Khmelnytskyi", "khmelnistkiy" } },
                { 23, new String[] { "Cherkasy", "cherkassy" } },
                { 24, new String[] { "Chernihiv", "chernigov" } },
                { 25, new String[] { "Chernivtsi", "chernovsty" } }
            };


        public static List<String> CITY_NAMES = new List<String>
        {
            Labels.City_All,
            Labels.City_Vinnitsia,
            Labels.City_Dnepr,
            Labels.City_Donetsk,
            Labels.City_Zhytomyr,
            Labels.City_Zp,
            Labels.City_IF,
            Labels.City_Kyiv,
            Labels.City_Kirovohrad,
            Labels.City_Luhansk,
            Labels.City_Volyn,
            Labels.City_Lviv,
            Labels.City_Mykolaiv,
            Labels.City_Odessa,
            Labels.City_Poltava,
            Labels.City_Rivne,
            Labels.City_Crimea,
            Labels.City_Sumy,
            Labels.City_Tern,
            Labels.City_Carpathian,
            Labels.City_Kharkiv,
            Labels.City_Kherson,
            Labels.City_Khmel,
            Labels.City_Cherkasy,
            Labels.City_Chernihiv,
            Labels.City_Chernivtsi
        };

        public static Dictionary<Int32, String> BANKS = new Dictionary<Int32, String> 
            { 
                { 0, "" },
                {1, "indexbank"},
				{2, "finance"},
				{3, "delta"},
				{4, "aval"},
				{5, "pumb"},
				{6, "vab"},
				{7, "imexbank"},
				{8, "alpha-bank"},
				{9, "otp"},
				{10, "finance_iniciativa"},
				{11, "pib"},
				{12, "pivdenny"},
				{13, "creditprombank"},
				{14, "piraeusbank"},
				{15, "unicredit"},
				{16, "forum"},
				{17, "xcitybank"},
				{18, "pivdencombank"},
				{19, "rodovidbank"},
				{20, "mercury-bank"},
				{21, "Tavrika"},
				{22, "a-bank"},
				{23, "activebank"},
            };

        public static List<String> BANK_NAMES = new List<String>
        {
            Labels.Bank_All,
            Labels.Bank_CreditAgricole,
            Labels.Bank_Fin,
            Labels.Bank_Delta,
            Labels.Bank_Raiffaisen,
            Labels.Bank_Pumb,
            Labels.Bank_Vab,
            Labels.Bank_Imex,
            Labels.Bank_Alpha,
            Labels.Bank_Otp,
            Labels.Bank_Fin_initiative,
            Labels.Bank_Prominvest,
            Labels.Bank_Pivdennyi,
            Labels.Bank_Creditprom,
            Labels.Bank_Pireus,
            Labels.Bank_Unicredit,
            Labels.Bank_Forum,
            Labels.Bank_Khreshchatyk,
            Labels.Bank_Pivdencom,
            Labels.Bank_Rodovid,
            Labels.Bank_Mercury,
            Labels.Bank_Tavrika,
            Labels.Bank_Akcent,
            Labels.Bank_Active,
        };
    }
}
