using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Model
{
    public class MyLotteryData
    {
        private string identifiers="";

        public string Identifiers
        {
            get { return identifiers; }
            set { identifiers = value; }
        }
        private string handleResult="";

        public string HandleResult
        {
            get { return handleResult; }
            set { handleResult = value; }
        }
        private string printResult="";

        public string PrintResult
        {
            get { return printResult; }
            set { printResult = value; }
        }
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int zhuShu;

        public int ZhuShu
        {
            get { return zhuShu; }
            set { zhuShu = value; }
        }
        private decimal money;

        public decimal Money
        {
            get { return money; }
            set { money = value; }
        }
        private int multiple;

        public int Multiple
        {
            get { return multiple; }
            set { multiple = value; }
        }
        private string number;

        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        private int lotteryId;

        public int LotteryId
        {
            get { return lotteryId; }
            set { lotteryId = value; }
        }
        private int playTypeID;

        public int PlayTypeID
        {
            get { return playTypeID; }
            set { playTypeID = value; }
        }
        private DateTime dtm;

        public DateTime Dtm
        {
            get { return dtm; }
            set { dtm = value; }
        }
        private string touZhuValue;

        public string TouZhuValue
        {
            get { return touZhuValue; }
            set { touZhuValue = value; }
        }
    }
}
