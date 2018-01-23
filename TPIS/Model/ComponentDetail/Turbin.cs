using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIS.Model.Common;

namespace TPIS.Model.ComponentDetail
{
    class Turbin : TPISComponent
    {
        public Turbin( int id, int tx, int ty, int width, int height, ComponentType ct) : base(tx, ty, width, height, ct)
        {
            //Nwidth = new List<int> { 4, 4, 4, 4, 4, 4, 4 };
            //Nheight = new List<int> { 8, 10, 12, 14, 16, 18, 20 };
            Pic = "/PNG/Turbin1.png";
            Id = id;
            eleType = EleType.Turbin;

            //CalMode DesignM = new CalMode(SelMode.设计模式); //设计
            //CalMode InterM = new CalMode(SelMode.插值模式); //插值
            //CalMode CalM = new CalMode(SelMode.计算模式); //计算
            //LCalMode.Add(DesignM); LCalMode.Add(InterM); LCalMode.Add(CalM);

            //AddPropetry(new Property("元件名称", "汽轮机", Units.NA, P_Type.ToSetAsString,"Name",""), "基本");
            //AddPropetry(new Property("排汽压力", 0, Units.MPaa, P_Type.ToSetAsDouble, "OutsteamP", ""), "基本");
            //Property OutsteamP = new Property("排汽压力", 0, Units.MPaa, P_Type.ToSetAsDouble, "基本");
            //OutsteamP.pcolor = PColor.Super;
            //DProperty.Add("OutsteamP", OutsteamP);

            //AddPropetry(new Property("出口焓值", 0, Units.KJ_KG, P_Type.ToSetAsDouble, "OutsteamH", ""), "基本");
            //OutsteamH.Tips = "至少要精确到小数点后一位";
            //OutsteamH.pcolor = PColor.Weak;
            //DProperty.Add("OutsteamH", OutsteamH);
            //Property OutsteamT = new Property("出口温度", 0, UnitEnum.Temperate, P_Type.ToSetAsDouble, "基本");
            //OutsteamT.Tips = "有焓值输入则此输入值无效\r\n过热度需大于15℃\r\n最好精确到小数点后三位";
            //OutsteamT.pcolor = PColor.Weak;
            //DProperty.Add("OutsteamT", OutsteamT);

            //DesignM.LDProperty.Add(Name); DesignM.LDProperty.Add(OutsteamP); DesignM.LDProperty.Add(OutsteamH); DesignM.LDProperty.Add(OutsteamT);

            //List<string> selected2 = new List<string>() { "质量流量效率", "体积流量效率", "压比效率", "固定效率" };
            //Property QVselect = new Property("效率插值选择", 0, "质量流量效率", P_Type.ToSetAsDouble, "汽轮机设计");
            //QVselect.Tips = "默认为质量流量效率\r\n体积流量为级组进出口体积流量的平均值";
            //QVselect.SelectList = selected2;
            //DProperty.Add("QVselect", QVselect);

            //List<string> selected1 = new List<string>() { "流量压力", "流量压比" };
            //Property QPselect = new Property("压力插值选择", 0, "流量压力", P_Type.ToSetAsDouble, "汽轮机设计");
            //QPselect.Tips = "默认为质量流量进汽压力\r\n流量都为质量流量";
            //QPselect.SelectList = selected1;
            //DProperty.Add("QPselect", QPselect);

            //Property PN = new Property("内部级数", 0, Units.jishu, P_Type.ToSetAsDouble, "汽轮机设计");
            //DProperty.Add("PN", PN);

            //Property ARFA = new Property("重热系数", 0, Units.NA, P_Type.ToSetAsDouble, "汽轮机设计");
            //ARFA.Tips = "一般为1.02 - 1.08";
            //DProperty.Add("ARFA", ARFA);

            //Property PE = new Property("部分进汽度", 0, Units.NA, P_Type.ToSetAsDouble, "汽轮机设计");
            //DProperty.Add("PE", PE);

            //Property PitchD = new Property("节圆直径", 0, Units.m, P_Type.ToSetAsDouble, "汽轮机设计");
            //PitchD.pcolor = PColor.Weak;
            //DProperty.Add("PitchD", PitchD);

            //Property OM0 = new Property("反动度", 0, Units.NA, P_Type.ToSetAsDouble, "汽轮机设计");
            //OM0.Data_string = "0.05";
            //OM0.Tips = "默认值为0.05";
            //DProperty.Add("OM0", OM0);

            //Property RP = new Property("转速", 0, Units.r_min, P_Type.ToSetAsDouble, "汽轮机设计");
            //RP.Data_string = "3000";
            //RP.Tips = "默认值为3000";
            //DProperty.Add("RP", RP);

            //Property PHI = new Property("喷嘴速度系数", 0, Units.NA, P_Type.ToSetAsDouble, "汽轮机设计");
            //PHI.Data_string = "0.97";
            //PHI.Tips = "默认值为0.97";
            //DProperty.Add("PHI", PHI);

            //Property PSI = new Property("动叶速度系数", 0, Units.NA, P_Type.ToSetAsDouble, "汽轮机设计");
            //PSI.Data_string = "0.93";
            //PSI.Tips = "默认值为0.93";
            //DProperty.Add("PSI", PSI);

            //Property ALPHA1 = new Property("静叶出口汽流角", 0, Units.jiaodu, P_Type.ToSetAsDouble, "汽轮机设计");
            //ALPHA1.Data_string = "15";
            //ALPHA1.Tips = "默认值为15";
            //DProperty.Add("ALPHA1", ALPHA1);

            //Property BETA2 = new Property("动叶出口汽流角", 0, Units.jiaodu, P_Type.ToSetAsDouble, "汽轮机设计");
            //BETA2.Data_string = "18";
            //BETA2.Tips = "默认值为18";
            //DProperty.Add("BETA2", BETA2);
            //CalM.LDProperty.AddRange(new List<Property> { Name, PN, ARFA, PE, PitchD, OM0, RP, PHI, PSI, ALPHA1, BETA2 });

            //Property PEff = new Property("内效率", 0, Units.Percent, P_Type.ToSetAsDouble, "辅助");
            //PEff.Tips = "当设有出口焓值时此效率设置不起作用\r\n变工况下以及效率为基准";
            //DProperty.Add("Eff", PEff);
            //DesignM.LDProperty.Add(PEff);
            ////Property PQ = new Property("参考流量", 0, Units.t_h, P_Type.ToSetAsDouble, "辅助");
            ////PQ.Tips = "设置后则变工况下以此流量为基准流量\r\n算级后压力";
            ////DProperty.Add("PQ", PQ);

            //Property EPower = new Property("轴功率", 0, Units.kW, P_Type.ToCal);
            //DPResult.Add("EPower", EPower);
            //Property Eff = new Property("内效率", 0, Units.Percent, P_Type.ToCal);
            //DPResult.Add("Eff", Eff);
            //Property Xa = new Property("速比", 0, Units.NA, P_Type.ToCal);
            //DPResult.Add("Xa", Xa);
            //Property RL = new Property("叶高", 0, Units.m, P_Type.ToCal);
            //DPResult.Add("RL", RL);
            //Property Mu = new Property("修正系数", 0, Units.NA, P_Type.ToCal, "辅助设计");
            //DPResult.Add("Mu", Mu);
            //Property QV = new Property("平均容积流量", 0, "m3/h", P_Type.ToCal, "辅助设计");
            //DPResult.Add("QV", QV);


            //InterM.LDProperty.AddRange(new List<Property> { Name, QVselect, PEff, QPselect });
            //XYDataLine qveff = new XYDataLine();
            //XYDataLine qeff = new XYDataLine();
            //XYDataLine paieff = new XYDataLine();
            //XYDataLine Inpl = new XYDataLine();
            //XYDataLine qpai = new XYDataLine();
            //InterM.Add("qvEff", "体积流量-效率", "m3/h", Units.Percent, qveff, DLines);
            //InterM.Add("Eff", "质量流量-效率", Units.t_h, Units.Percent, qeff, DLines);
            //InterM.Add("paiEff", "压比-效率", "出/进", Units.Percent, paieff, DLines);//压比写上谁比谁
            //InterM.Add("InP", "质量流量-进汽压力", Units.t_h, Units.MPa, Inpl, DLines);
            //InterM.Add("qpai", "体积流量-压比", "m3/h", "出/进", qpai, DLines);

            //InterM.LDPResult.AddRange(new List<Property> { Eff, EPower, QV });
            //CalM.LDPResult.AddRange(new List<Property> { Name, Eff, EPower, Xa, RL, Mu });
            //DesignM.LDPResult.AddRange(new List<Property> { Eff, EPower, QV });


            //Nozzle N1 = new Nozzle(Material.water, NodType.Inlet, false);
            //N1.Name = "进口蒸汽";
            //N1.SetNXY(new List<int> { 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 1, 1, 1, 1, 1, 1, 1 });
            //IOPoints.Add("InSteam", N1);

            //Nozzle N2 = new Nozzle(Material.water, NodType.Outlet, false);
            //N2.Name = "出口蒸汽";
            //N2.SetNXY(new List<int> { 4, 4, 4, 4, 4, 4, 4 }, new List<int> { 0, 0, 0, 0, 0, 0, 0 });
            //IOPoints.Add("OutSteam", N2);

            //Nozzle Ninner = new Nozzle(Material.water, NodType.Outlet, false);
            //Ninner.IsInner = true;
            //IOPoints.Add("InnerSteam", Ninner);

            //Nozzle N3 = new Nozzle(Material.power, NodType.Inlet, true);
            //N3.Name = "输入功率";
            //N3.SetNXY(new List<int> { 0, 0, 0, 0, 0, 0, 0 }, new List<int> { 4, 5, 6, 7, 8, 9, 10 });
            //IOPoints.Add("InPower", N3);

            //Nozzle N4 = new Nozzle(Material.power, NodType.Outlet, false);
            //N4.Name = "输出功率";
            //N4.SetNXY(new List<int> { 4, 4, 4, 4, 4, 4, 4 }, new List<int> { 4, 5, 6, 7, 8, 9, 10 });
            //IOPoints.Add("Outpower", N4);

            //Nozzle N5 = new Nozzle(Material.water, NodType.Outlet, true);
            //N5.Name = "抽汽点";
            //N5.SetNXY(new List<int> { 4, 4, 4, 4, 4, 4, 4 }, new List<int> { 8, 10, 12, 14, 16, 18, 20 });
            //N5.CanCancel = true;
            //IOPoints.Add("ExtractSteam", N5);

            ////InterM.LDPResult.Add(IOPoints["InSteam"].DProperty["P"]);
            //eleSpec.IsShow = false;
        }

        
    }
}
