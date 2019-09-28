using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jjson
{
    public class Createson
    {
        public ProFund proFun;
        public ComExp comexp;
        public Intro intro;
        public MathEngI mathI;
        public PhyI phyI;
        public Lab_phyI labphyI;
        public TableTennis tabletennis;
        public EnglishI engI;
        public ManSo manso;
        public Circuit circuit;
        public LabCir lab_cir;
        public Basketball bas;
        public EngII engII;
        public WorkE workEt;
        public MathII mathII;
        public PhyII phyII;
        public LabPhyII labphyII;
        public Algo_data data;
        public Soft_DevI software;
        public Signals signal;
        public Digital digi;
        public Lab_Digi labDigi;
        public Discrete discrete;
        public Statistics stat;
        public Phy_life phylife;
        public Gen_Math genMath;
        public Psychology psychology;
        public Eng_Conv engConv;
        public English_skill engSkill;
        public Biology bio;
        public Volley voll;
        public Economy econ;
        public Economics economics;
        public National nation;
        public Business business;
        public Art_app art;
        public Analog anolog;
        public Com_Organ comOR;
        public Soft_DevII softwareII;
        public Network network;
        public Ubiquitous Ubq;
        public Soft_Eng softEng;
        public Lab_analog lab_Analog;
        public OS os;
        public Embedded embedded;
        public Network_II networkII;
        public Lab_emb labEmb;
        public Database database;
        public Lab_Network labNetwork;
        public Project_I proJ_1;
        public Project_II proJ_2;
        public Com_eng com_Eng;
        public Numerical numerical;
        public Analysis_Algo analysis_Algo;
        public Intro_Control introControl;
        public Security security;

    }
    public class JsonData
    {
        public Createson CreatesonOps()
        {
            Createson JsonObj = new Createson();
            ProFund profunObj = new ProFund();
            JsonObj.proFun = profunObj;
            ComExp ComexObj = new ComExp();
            JsonObj.comexp = ComexObj;
            Intro IntroObj = new Intro();
            JsonObj.intro = IntroObj;
            MathEngI mathI_Obj = new MathEngI();
            JsonObj.mathI = mathI_Obj;
            PhyI phyI_obj = new PhyI();
            JsonObj.phyI = phyI_obj;
            Lab_phyI labphyI_obj = new Lab_phyI();
            JsonObj.labphyI = labphyI_obj;
            TableTennis Tbtennis_Obj = new TableTennis();
            JsonObj.tabletennis = Tbtennis_Obj;
            EnglishI engI_obj = new EnglishI();
            JsonObj.engI = engI_obj;
            ManSo manObj = new ManSo();
            JsonObj.manso = manObj;
            Circuit cir_obj = new Circuit();
            JsonObj.circuit = cir_obj;
            LabCir labcir_obj = new LabCir();
            JsonObj.lab_cir = labcir_obj;
            Basketball bas_obj = new Basketball();
            JsonObj.bas = bas_obj;
            EngII engII_obj = new EngII();
            JsonObj.engII = engII_obj;
            WorkE workE_obj = new WorkE();
            JsonObj.workEt = workE_obj;
            MathII mathII_obj = new MathII();
            JsonObj.mathII = mathII_obj;
            PhyII phyII_obj = new PhyII();
            JsonObj.phyII = phyII_obj;
            LabPhyII labphyII_obj = new LabPhyII();
            JsonObj.labphyII = labphyII_obj;
            Algo_data data_obj = new Algo_data();
            JsonObj.data = data_obj;
            Soft_DevI software_obj = new Soft_DevI();
            JsonObj.software = software_obj;
            Signals signal_obj = new Signals();
            JsonObj.signal = signal_obj;
            Digital digi_obj = new Digital();
            JsonObj.digi = digi_obj;
            Lab_Digi labdigi_obj = new Lab_Digi();
            JsonObj.labDigi = labdigi_obj;
            Discrete dis_obj = new Discrete();
            JsonObj.discrete = dis_obj;
            Statistics stat_obj = new Statistics();
            JsonObj.stat = stat_obj;
            Phy_life phyLife_obj = new Phy_life();
            JsonObj.phylife = phyLife_obj;
            Gen_Math genMath_obj = new Gen_Math();
            JsonObj.genMath = genMath_obj;
            Psychology psy_obj = new Psychology();
            JsonObj.psychology = psy_obj;
            Economics economics_obj = new Economics();
            JsonObj.economics = economics_obj;
            Economy econ_obj = new Economy();
            JsonObj.econ = econ_obj;
            Eng_Conv engC_obj = new Eng_Conv();
            JsonObj.engConv = engC_obj;
            English_skill engSkill_obj = new English_skill();
            JsonObj.engSkill = engSkill_obj;
            Volley voll_obj = new Volley();
            JsonObj.voll = voll_obj;
            Biology bio_obj = new Biology();
            JsonObj.bio = bio_obj;
            National nation_obj = new National();
            JsonObj.nation = nation_obj;
            Business business_obj = new Business();
            JsonObj.business = business_obj;
            Art_app art_obj = new Art_app();
            JsonObj.art = art_obj;
            Analog analog_obj = new Analog();
            JsonObj.anolog = analog_obj;
            Soft_DevII softII_obj = new Soft_DevII();
            JsonObj.softwareII = softII_obj;
            Com_Organ comOr_obj = new Com_Organ();
            JsonObj.comOR = comOr_obj;
            Network net_obj = new Network();
            JsonObj.network = net_obj;
            Ubiquitous ubq_obj = new Ubiquitous();
            JsonObj.Ubq = ubq_obj;
            Soft_Eng softEng_obj = new Soft_Eng();
            JsonObj.softEng = softEng_obj;
            Lab_analog labAna_obj = new Lab_analog();
            JsonObj.lab_Analog = labAna_obj;
            OS os_obj = new OS();
            JsonObj.os = os_obj;
            Embedded emb_obj = new Embedded();
            JsonObj.embedded = emb_obj;
            Network_II netII_obj = new Network_II();
            JsonObj.networkII = netII_obj;
            Lab_emb labEmb_obj = new Lab_emb();
            JsonObj.labEmb = labEmb_obj;
            Database database_obj = new Database();
            JsonObj.database = database_obj;
            Lab_Network labNet_obj = new Lab_Network();
            JsonObj.labNetwork = labNet_obj;
            Project_I proJ1_obj = new Project_I();
            JsonObj.proJ_1 = proJ1_obj;
            Project_II proJ2_obj = new Project_II();
            JsonObj.proJ_2 = proJ2_obj;
            Com_eng comEng_obj = new Com_eng();
            JsonObj.com_Eng = comEng_obj;
            Numerical num_obj = new Numerical();
            JsonObj.numerical = num_obj;
            Analysis_Algo ana_obj = new Analysis_Algo();
            JsonObj.analysis_Algo = ana_obj;
            Intro_Control introCon_obj = new Intro_Control();
            JsonObj.introControl = introCon_obj;
            Security scy_obj = new Security();
            JsonObj.security = scy_obj;
            return JsonObj;
        }
    }
    public class ProFund
    {
        public string Type = "Main";
        public string ID = "01012302";
        public string Name_subject = "Programming Fundamentals";
        public string Semester = "1";
        public string Status;
        public string Description = "Profun";
        public string weight = "3";
        public string PreRequired;
    }
    public class ComExp
    {
        public string Type = "Main";
        public string ID = "010123130";
        public string Name_subject = "Computer Engineering Exploration";
        public string Semester = "1";
        public string Status;
        public string Description = "ComExplo";
        public string weight = "1";
        public string PreRequired;
    }
    public class Intro
    {
        public string Type = "Main";
        public string ID = "010403005";
        public string Name_subject = "Introduction to Engineering";
        public string Semester = "1";
        public string Status;
        public string Description = "Intro";
        public string weight = "1";
        public string PreRequired;
    }
    public class MathEngI
    {
        public string Type = "Main";
        public string ID = "040203111";
        public string Name_subject = "Engineering  Mathematics I";
        public string Semester = "1";
        public string Status;
        public string Description = "MathI";
        public string weight = "3";
        public string PreRequired;
    }
    public class PhyI
    {
        public string Type = "Main";
        public string ID = "040313005";
        public string Name_subject = "Physics I";
        public string Semester = "1";
        public string Status;
        public string Description = "Phy I";
        public string weight = "3";
        public string PreRequired;
    }
    public class Lab_phyI
    {
        public string Type = "Main";
        public string ID = "040313006";
        public string Name_subject = "Physics Laboratory I";
        public string Semester = "1";
        public string Status;
        public string Description = "Lab Phy I";
        public string weight = "1";
        public string PreRequired;
    }
    public class TableTennis
    {
        public string Type = "Elective(Main)";
        public string ID = "080303505";
        public string Name_subject = "Table Tennis";
        public string Semester = "1";
        public string Status;
        public string Description = "Tabletennis";
        public string weight = "1";
        public string PreRequired;
    }
    public class EnglishI
    {
        public string Type = "Elective(Main)";
        public string ID = "080103001";
        public string Name_subject = "English I";
        public string Semester = "1";
        public string Status;
        public string Description = "Eng I";
        public string weight = "3";
        public string PreRequired;
    }
    public class ManSo
    {
        public string Type = "Elective(Main)";
        public string ID = "080203901";
        public string Name_subject = "Man and Social";
        public string Semester = "1";
        public string Status;
        public string Description = "ManSo";
        public string weight = "3";
        public string PreRequired;
    }
    public class Circuit
    {
        public string Type = "Main";
        public string ID = "010113010";
        public string Name_subject = "Electric Circuit Theory";
        public string Semester = "2";
        public string Status;
        public string Description = "Circuit";
        public string weight = "3";
        public string PreRequired;
    }
    public class LabCir
    {
        public string Type = "Main";
        public string ID = "010113011";
        public string Name_subject = "Electric Circuit Laboratory";
        public string Semester = "2";
        public string Status;
        public string Description = "LabCircuit";
        public string weight = "1";
        public string PreRequired;
    }
    public class Basketball
    {
        public string Type = "Elective(Main)";
        public string ID = "080303501";
        public string Name_subject = "Basketball";
        public string Semester = "2";
        public string Status;
        public string Description = "Bas";
        public string weight = "1";
        public string PreRequired;
    }
    public class EngII
    {
        public string Type = "Elective(Main)";
        public string ID = "080103002";
        public string Name_subject = "English II";
        public string Semester = "2";
        public string Status;
        public string Description = "EngII";
        public string weight = "3";
        public string PreRequired = "080103001";
    }
    public class WorkE
    {
        public string Type = "Main";
        public string ID = "010403006";
        public string Name_subject = "Work Ethics";
        public string Semester = "2";
        public string Status;
        public string Description = "Work Ethics";
        public string weight = "1";
        public string PreRequired;
    }
    public class MathII
    {
        public string Type = "Main";
        public string ID = "040203112";
        public string Name_subject = "Engineering Mathematics II";
        public string Semester = "2";
        public string Status;
        public string Description = "Math II";
        public string weight = "3";
        public string PreRequired = "040203111";
    }
    public class PhyII
    {
        public string Type = "Main";
        public string ID = "040313007";
        public string Name_subject = "Physics II";
        public string Semester = "2";
        public string Status;
        public string Description = "Phy II";
        public string weight = "3";
        public string PreRequired = "040313005";
    }
    public class LabPhyII
    {
        public string Type = "Main";
        public string ID = "040313008";
        public string Name_subject = "Physics Laboratory II";
        public string Semester = "2";
        public string Status;
        public string Description = "LabPhy II";
        public string weight = "1";
        public string PreRequired = "040313006";
    }
    public class Algo_data
    {
        public string Type = "Main";
        public string ID = "010123103";
        public string Name_subject = "Algorithms and Data Structures";
        public string Semester = "2";
        public string Status;
        public string Description = "data";
        public string weight = "3";
        public string PreRequired = "010123102";
    }
    public class Soft_DevI
    {
        public string Type = "Main";
        public string ID = "010123131";
        public string Name_subject = "Software Development Practice I";
        public string Semester = "3";
        public string Status;
        public string Description = "Software";
        public string weight = "3";
        public string PreRequired = "010123103";
    }
    public class Signals
    {
        public string Type = "Main";
        public string ID = "010123106";
        public string Name_subject = "Introduction to Signals and Systems";
        public string Semester = "3";
        public string Status;
        public string Description = "Signal";
        public string weight = "3";
        public string PreRequired = "040203112";
    }
    public class Digital
    {
        public string Type = "Main";
        public string ID = "01012307";
        public string Name_subject = "Logic Design of Digital Systems";
        public string Semester = "3";
        public string Status;
        public string Description = "Digi";
        public string weight = "3";
        public string PreRequired;
    }
    public class Lab_Digi
    {
        public string Type = "Main";
        public string ID = "010123108";
        public string Name_subject = "Digital System Design Laboratory";
        public string Semester = "3";
        public string Status;
        public string Description = "Lab digi";
        public string weight = "1";
        public string PreRequired;
    }
    public class Discrete
    {
        public string Type = "Main";
        public string ID = "010123133";
        public string Name_subject = "Discrete Mathematics";
        public string Semester = "3";
        public string Status;
        public string Description = "Discrete";
        public string weight = "3";
        public string PreRequired = "040203112";
    }
    public class Statistics
    {
        public string Type = "Main";
        public string ID = "010123105";
        public string Name_subject = "Statistics for Computer Engineers";
        public string Semester = "3";
        public string Status;
        public string Description = "Stat";
        public string weight = "3";
        public string PreRequired = "040203111";
    }
    public class Phy_life
    {
        public string Type = "Elective(Art)";
        public string ID = "040313016";
        public string Name_subject = "Physics in Daily Life";
        public string Semester ;
        public string Status;
        public string Description = "Phy Life";
        public string weight = "3";
        public string PreRequired;
    }
    public class Gen_Math
    {
        public string Type = "Elective(Art)";
        public string ID = "040203100";
        public string Name_subject = "General Mathematics";
        public string Semester ;
        public string Status;
        public string Description = "Gen math";
        public string weight = "3";
        public string PreRequired;

    }
    public class Psychology
    {
        public string Type = "Elective(Art)";
        public string ID = "080303103";
        public string Name_subject = "Psychology for Happy Life";
        public string Semester ;
        public string Status;
        public string Description = "Happy Life";
        public string weight = "3";
        public string PreRequired;
    }
    public class Eng_Conv
    {
        public string Type = "Elective(Art)";
        public string ID = "080103016";
        public string Name_subject = "English Conversation I";
        public string Semester ;
        public string Status;
        public string Description = "Eng Con";
        public string weight = "3";
        public string PreRequired;
    }
    public class Biology
    {
        public string Type = "Elective(Art)";
        public string ID = "040413001";
        public string Name_subject = "Biology in Daily Life";
        public string Semester ;
        public string Status;
        public string Description = "Bio";
        public string weight = "3";
        public string PreRequired;
    }
    public class Volley
    {
        public string Type = "Elective(Art)";
        public string ID = "080303502";
        public string Name_subject = "Volleyball";
        public string Semester ;
        public string Status;
        public string Description = "Volley";
        public string weight = "1";
        public string PreRequired;
    }
    public class National
    {
        public string Type = "Elective(Art)";
        public string ID = "080203902";
        public string Name_subject = "Nation Heritage and Civilization";
        public string Semester ;
        public string Status;
        public string Description = "Nation";
        public string weight = "3";
        public string PreRequired;
    }
    public class Economy
    {
        public string Type = "Elective(Art)";
        public string ID = "080203905";
        public string Name_subject = "Economy and Everyday Life";
        public string Semester;
        public string Status;
        public string Description = "Econ";
        public string weight = "3";
        public string PreRequired;
    }
    public class Business
    {
        public string Type = "Elective(Art)";
        public string ID = "080203907";
        public string Name_subject = "Business and Everyday Life";
        public string Semester ;
        public string Status;
        public string Description = "Business";
        public string weight = "3";
        public string PreRequired;
    }
    public class Economics
    {
        public string Type = "Elective(Art)";
        public string ID = "080203906";
        public string Name_subject = "Economics for Individual Development";
        public string Semester;
        public string Status;
        public string Description = "Economics";
        public string weight = "3";
        public string PreRequired;
    }
    public class Art_app
    {
        public string Type = "Elective(Art)";
        public string ID = "080303301";
        public string Name_subject = "Art Appreciation";
        public string Semester ;
        public string Status;
        public string Description = "Art";
        public string weight = "3";
        public string PreRequired;
    }
    public class English_skill
    {
        public string Type = "Elective(Art)";
        public string ID = "080103011";
        public string Name_subject = "English Study Skill";
        public string Semester ;
        public string Status;
        public string Description = "Eng skill";
        public string weight = "3";
        public string PreRequired;
    }
    public class Analog
    {
        public string Type = "Main";
        public string ID = "010123114";
        public string Name_subject = "Analog and Digital Electronics";
        public string Semester ="4";
        public string Status;
        public string Description = "Analog";
        public string weight = "3";
        public string PreRequired ="040203111";
    }
    public class Network
    {
        public string Type = "Main";
        public string ID = "010123126";
        public string Name_subject = "Computer Networks I";
        public string Semester = "4";
        public string Status;
        public string Description = "Network";
        public string weight = "3";
        public string PreRequired ;
    }
    public class Ubiquitous
    {
        public string Type = "Main";
        public string ID = "010123129";
        public string Name_subject = "Ubiquitous Computing";
        public string Semester = "4";
        public string Status;
        public string Description = "Ubiquitous";
        public string weight = "3";
        public string PreRequired ;
    }
    public class Soft_DevII
    {
        public string Type = "Main";
        public string ID = "010123132";
        public string Name_subject = "Software Development Pratice II";
        public string Semester = "4";
        public string Status;
        public string Description = "Soft Dev II";
        public string weight = "3";
        public string PreRequired = "010123131";
    }
    public class Com_Organ
    {
        public string Type = "Main";
        public string ID = "010123134";
        public string Name_subject = "Computer Organization";
        public string Semester = "4";
        public string Status;
        public string Description = "Com OR";
        public string weight = "3";
        public string PreRequired = "010123107";
    }
    public class Soft_Eng
    {
        public string Type = "Main";
        public string ID = "010123116";
        public string Name_subject = "Software Engineering";
        public string Semester = "5";
        public string Status;
        public string Description = "Soft Eng";
        public string weight = "3";
        public string PreRequired = "010123132";
    }
    public class Lab_analog
    {
        public string Type = "Main";
        public string ID = "010123115";
        public string Name_subject = "Analog and Digital Electronics Laboratory";
        public string Semester = "5";
        public string Status;
        public string Description = "Lab Analog";
        public string weight = "1";
        public string PreRequired = "010123115";
    }
    public class Network_II
    {
        public string Type = "Main";
        public string ID = "010123127";
        public string Name_subject = "Computer Networks II";
        public string Semester = "5";
        public string Status;
        public string Description = "Network II";
        public string weight = "3";
        public string PreRequired = "010123126";
    }
    public class OS
    {
        public string Type = "Main";
        public string ID = "010123117";
        public string Name_subject = "Operating Systems";
        public string Semester = "5";
        public string Status;
        public string Description = "OS";
        public string weight = "3";
        public string PreRequired = "010123102";
    }
    public class Embedded
    {
        public string Type = "Main";
        public string ID = "010123119";
        public string Name_subject = "Embedded System Design";
        public string Semester = "5";
        public string Status;
        public string Description = "Embed";
        public string weight = "3";
        public string PreRequired = "010123134";
    }
    public class Lab_emb
    {
        public string Type = "Main";
        public string ID = "010123120";
        public string Name_subject = "Embedded System Design Laboratory";
        public string Semester = "6";
        public string Status;
        public string Description = "Lab Embed";
        public string weight = "1";
        public string PreRequired = "010123119";
    }
    public class Database
    {
        public string Type = "Main";
        public string ID = "010123121";
        public string Name_subject = "Database Systems";
        public string Semester = "6";
        public string Status;
        public string Description = "Database";
        public string weight = "3";
        public string[] PreRequired = { "010123103","010123133" };
    }
    public class Lab_Network
    {
        public string Type = "Main";
        public string ID = "010123128";
        public string Name_subject = "Computer Networks Laboratory";
        public string Semester = "6";
        public string Status;
        public string Description = "Lab Network";
        public string weight = "1";
        public string PreRequired = "010123127";
    }
    public class Project_I
    {
        public string Type = "Main";
        public string ID = "010113941";
        public string Name_subject = "Project I";
        public string Semester = "7";
        public string Status;
        public string Description = "Project 1";
        public string weight = "3";
        public string PreRequired ;
    }
    public class Project_II
    {
        public string Type = "Main";
        public string ID = "010113942";
        public string Name_subject = "Project II";
        public string Semester = "8";
        public string Status;
        public string Description = "Project 2";
        public string weight = "3";
        public string PreRequired = "010113941";
    }
    public class Com_eng
    {
        public string Type = "Main";
        public string ID = "010123124";
        public string Name_subject = "Computer Engineering Seminar";
        public string Semester = "8";
        public string Status;
        public string Description = "Com Eng";
        public string weight = "2";
        public string PreRequired ;
    }
    public class Numerical
    {
        public string Type = "Elective(Main)";
        public string ID = "010123201";
        public string Name_subject = "Numerical Methods for Computer Engineers";
        public string[] Semester = { "3", "4" };
        public string Status;
        public string Description = "Num Method";
        public string weight = "3";
        public string PreRequired ;
    }
    public class Analysis_Algo
    {
        public string Type = "Elective(Main)";
        public string ID = "010123203";
        public string Name_subject = "Analysis and Design of Algorithms";
        public string[] Semester = { "3", "4" };
        public string Status;
        public string Description = "Analysis";
        public string weight = "3";
        public string PreRequired;
    }
    public class Intro_Control
    {
        public string Type = "Elective(Main)";
        public string ID = "010123204";
        public string Name_subject = "Introduction to Control Engineering";
        public string[] Semester = { "3", "4" };
        public string Status;
        public string Description = "Intro Control";
        public string weight = "3";
        public string PreRequired;
    }
    public class Security
    {
        public string Type = "Elective(Main)";
        public string ID = "010123205";
        public string Name_subject = "Computer and Network Security";
        public string[] Semester = { "3", "4" };
        public string Status;
        public string Description = "Security";
        public string weight = "3";
        public string PreRequired;
    }


}
