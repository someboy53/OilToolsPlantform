/****** Object:  UserDefinedFunction [dbo].[fn_GetPying]    Script Date: 01/04/2017 11:07:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 根据汉字获取全拼
 1.生成所有读音临时表
 2.根据Chinese_PRC_CS_AS_KS_WS 排序获取读音
*/
ALTER FUNCTION [dbo].[fn_getPying] ( @str VARCHAR(100) )
RETURNS VARCHAR(8000)
AS
    BEGIN
        DECLARE @re VARCHAR(8000);
 --生成临时表
        DECLARE @t TABLE
            (
              chr NCHAR(1) COLLATE Chinese_PRC_CS_AS_KS_WS ,
              py NVARCHAR(20)
            ); 
        INSERT  INTO @t
                SELECT  '吖' ,
                        'a'; 
        INSERT  INTO @t
                SELECT  '厑' ,
                        'aes'; 
        INSERT  INTO @t
                SELECT  '哎' ,
                        'ai'; 
        INSERT  INTO @t
                SELECT  '安' ,
                        'an'; 
        INSERT  INTO @t
                SELECT  '肮' ,
                        'ang'; 
        INSERT  INTO @t
                SELECT  '凹' ,
                        'ao'; 
        INSERT  INTO @t
                SELECT  '八' ,
                        'ba'; 
        INSERT  INTO @t
                SELECT  '挀' ,
                        'bai'; 
        INSERT  INTO @t
                SELECT  '兡' ,
                        'baike'; 
        INSERT  INTO @t
                SELECT  '瓸' ,
                        'baiwa'; 
        INSERT  INTO @t
                SELECT  '扳' ,
                        'ban'; 
        INSERT  INTO @t
                SELECT  '邦' ,
                        'bang'; 
        INSERT  INTO @t
                SELECT  '勹' ,
                        'bao'; 
        INSERT  INTO @t
                SELECT  '萡' ,
                        'be'; 
        INSERT  INTO @t
                SELECT  '陂' ,
                        'bei'; 
        INSERT  INTO @t
                SELECT  '奔' ,
                        'ben'; 
        INSERT  INTO @t
                SELECT  '伻' ,
                        'beng'; 
        INSERT  INTO @t
                SELECT  '皀' ,
                        'bi'; 
        INSERT  INTO @t
                SELECT  '边' ,
                        'bian'; 
        INSERT  INTO @t
                SELECT  '辪' ,
                        'uu'; 
        INSERT  INTO @t
                SELECT  '灬' ,
                        'biao'; 
        INSERT  INTO @t
                SELECT  '憋' ,
                        'bie'; 
        INSERT  INTO @t
                SELECT  '汃' ,
                        'bin'; 
        INSERT  INTO @t
                SELECT  '冫' ,
                        'bing'; 
        INSERT  INTO @t
                SELECT  '癶' ,
                        'bo'; 
        INSERT  INTO @t
                SELECT  '峬' ,
                        'bu'; 
        INSERT  INTO @t
                SELECT  '嚓' ,
                        'ca'; 
        INSERT  INTO @t
                SELECT  '偲' ,
                        'cai'; 
        INSERT  INTO @t
                SELECT  '乲' ,
                        'cal'; 
        INSERT  INTO @t
                SELECT  '参' ,
                        'can'; 
        INSERT  INTO @t
                SELECT  '仓' ,
                        'cang'; 
        INSERT  INTO @t
                SELECT  '撡' ,
                        'cao'; 
        INSERT  INTO @t
                SELECT  '冊' ,
                        'ce'; 
        INSERT  INTO @t
                SELECT  '膥' ,
                        'cen'; 
        INSERT  INTO @t
                SELECT  '噌' ,
                        'ceng'; 
        INSERT  INTO @t
                SELECT  '硛' ,
                        'ceok'; 
        INSERT  INTO @t
                SELECT  '岾' ,
                        'ceom'; 
        INSERT  INTO @t
                SELECT  '猠' ,
                        'ceon'; 
        INSERT  INTO @t
                SELECT  '乽' ,
                        'ceor'; 
        INSERT  INTO @t
                SELECT  '叉' ,
                        'cha'; 
        INSERT  INTO @t
                SELECT  '犲' ,
                        'chai'; 
        INSERT  INTO @t
                SELECT  '辿' ,
                        'chan'; 
        INSERT  INTO @t
                SELECT  '伥' ,
                        'chang'; 
        INSERT  INTO @t
                SELECT  '抄' ,
                        'chao'; 
        INSERT  INTO @t
                SELECT  '车' ,
                        'che'; 
        INSERT  INTO @t
                SELECT  '抻' ,
                        'chen'; 
        INSERT  INTO @t
                SELECT  '阷' ,
                        'cheng'; 
        INSERT  INTO @t
                SELECT  '吃' ,
                        'chi'; 
        INSERT  INTO @t
                SELECT  '充' ,
                        'chong'; 
        INSERT  INTO @t
                SELECT  '抽' ,
                        'chou'; 
        INSERT  INTO @t
                SELECT  '出' ,
                        'chu'; 
        INSERT  INTO @t
                SELECT  '膗' ,
                        'chuai'; 
        INSERT  INTO @t
                SELECT  '巛' ,
                        'chuan'; 
        INSERT  INTO @t
                SELECT  '刅' ,
                        'chuang'; 
        INSERT  INTO @t
                SELECT  '吹' ,
                        'chui'; 
        INSERT  INTO @t
                SELECT  '旾' ,
                        'chun'; 
        INSERT  INTO @t
                SELECT  '踔' ,
                        'chuo'; 
        INSERT  INTO @t
                SELECT  '呲' ,
                        'ci'; 
        INSERT  INTO @t
                SELECT  '嗭' ,
                        'cis'; 
        INSERT  INTO @t
                SELECT  '从' ,
                        'cong'; 
        INSERT  INTO @t
                SELECT  '凑' ,
                        'cou'; 
        INSERT  INTO @t
                SELECT  '粗' ,
                        'cu'; 
        INSERT  INTO @t
                SELECT  '汆' ,
                        'cuan'; 
        INSERT  INTO @t
                SELECT  '崔' ,
                        'cui'; 
        INSERT  INTO @t
                SELECT  '邨' ,
                        'cun'; 
        INSERT  INTO @t
                SELECT  '瑳' ,
                        'cuo'; 
        INSERT  INTO @t
                SELECT  '撮' ,
                        'chua'; 
        INSERT  INTO @t
                SELECT  '咑' ,
                        'da'; 
        INSERT  INTO @t
                SELECT  '呔' ,
                        'dai'; 
        INSERT  INTO @t
                SELECT  '丹' ,
                        'dan'; 
        INSERT  INTO @t
                SELECT  '当' ,
                        'dang'; 
        INSERT  INTO @t
                SELECT  '刀' ,
                        'dao'; 
        INSERT  INTO @t
                SELECT  '恴' ,
                        'de'; 
        INSERT  INTO @t
                SELECT  '揼' ,
                        'dem'; 
        INSERT  INTO @t
                SELECT  '扥' ,
                        'den'; 
        INSERT  INTO @t
                SELECT  '灯' ,
                        'deng'; 
        INSERT  INTO @t
                SELECT  '仾' ,
                        'di'; 
        INSERT  INTO @t
                SELECT  '嗲' ,
                        'dia'; 
        INSERT  INTO @t
                SELECT  '敁' ,
                        'dian'; 
        INSERT  INTO @t
                SELECT  '刁' ,
                        'diao'; 
        INSERT  INTO @t
                SELECT  '爹' ,
                        'die'; 
        INSERT  INTO @t
                SELECT  '哋' ,
                        'dei'; 
        INSERT  INTO @t
                SELECT  '嚸' ,
                        'dim'; 
        INSERT  INTO @t
                SELECT  '丁' ,
                        'ding'; 
        INSERT  INTO @t
                SELECT  '丟' ,
                        'diu'; 
        INSERT  INTO @t
                SELECT  '东' ,
                        'dong'; 
        INSERT  INTO @t
                SELECT  '吺' ,
                        'dou'; 
        INSERT  INTO @t
                SELECT  '剢' ,
                        'du'; 
        INSERT  INTO @t
                SELECT  '耑' ,
                        'duan'; 
        INSERT  INTO @t
                SELECT  '叾' ,
                        'dug'; 
        INSERT  INTO @t
                SELECT  '垖' ,
                        'dui'; 
        INSERT  INTO @t
                SELECT  '吨' ,
                        'dun'; 
        INSERT  INTO @t
                SELECT  '咄' ,
                        'duo'; 
        INSERT  INTO @t
                SELECT  '妸' ,
                        'e'; 
        INSERT  INTO @t
                SELECT  '奀' ,
                        'en'; 
        INSERT  INTO @t
                SELECT  '鞥' ,
                        'eng'; 
        INSERT  INTO @t
                SELECT  '仒' ,
                        'eo'; 
        INSERT  INTO @t
                SELECT  '乻' ,
                        'eol'; 
        INSERT  INTO @t
                SELECT  '旕' ,
                        'eos'; 
        INSERT  INTO @t
                SELECT  '儿' ,
                        'er'; 
        INSERT  INTO @t
                SELECT  '发' ,
                        'fa'; 
        INSERT  INTO @t
                SELECT  '帆' ,
                        'fan'; 
        INSERT  INTO @t
                SELECT  '匚' ,
                        'fang'; 
        INSERT  INTO @t
                SELECT  '飞' ,
                        'fei'; 
        INSERT  INTO @t
                SELECT  '吩' ,
                        'fen'; 
        INSERT  INTO @t
                SELECT  '丰' ,
                        'feng'; 
        INSERT  INTO @t
                SELECT  '瓰' ,
                        'fenwa'; 
        INSERT  INTO @t
                SELECT  '覅' ,
                        'fiao'; 
        INSERT  INTO @t
                SELECT  '仏' ,
                        'fo'; 
        INSERT  INTO @t
                SELECT  '垺' ,
                        'fou'; 
        INSERT  INTO @t
                SELECT  '夫' ,
                        'fu'; 
        INSERT  INTO @t
                SELECT  '猤' ,
                        'fui'; 
        INSERT  INTO @t
                SELECT  '旮' ,
                        'ga'; 
        INSERT  INTO @t
                SELECT  '侅' ,
                        'gai'; 
        INSERT  INTO @t
                SELECT  '甘' ,
                        'gan'; 
        INSERT  INTO @t
                SELECT  '冈' ,
                        'gang'; 
        INSERT  INTO @t
                SELECT  '皋' ,
                        'gao'; 
        INSERT  INTO @t
                SELECT  '戈' ,
                        'ge'; 
        INSERT  INTO @t
                SELECT  '给' ,
                        'gei'; 
        INSERT  INTO @t
                SELECT  '根' ,
                        'gen'; 
        INSERT  INTO @t
                SELECT  '更' ,
                        'geng'; 
        INSERT  INTO @t
                SELECT  '啹' ,
                        'geu'; 
        INSERT  INTO @t
                SELECT  '喼' ,
                        'gib'; 
        INSERT  INTO @t
                SELECT  '嗰' ,
                        'go'; 
        INSERT  INTO @t
                SELECT  '工' ,
                        'gong'; 
        INSERT  INTO @t
                SELECT  '兝' ,
                        'gongfen'; 
        INSERT  INTO @t
                SELECT  '兣' ,
                        'gongli'; 
        INSERT  INTO @t
                SELECT  '勾' ,
                        'gou'; 
        INSERT  INTO @t
                SELECT  '估' ,
                        'gu'; 
        INSERT  INTO @t
                SELECT  '瓜' ,
                        'gua'; 
        INSERT  INTO @t
                SELECT  '乖' ,
                        'guai'; 
        INSERT  INTO @t
                SELECT  '关' ,
                        'guan'; 
        INSERT  INTO @t
                SELECT  '光' ,
                        'guang'; 
        INSERT  INTO @t
                SELECT  '归' ,
                        'gui'; 
        INSERT  INTO @t
                SELECT  '丨' ,
                        'gun'; 
        INSERT  INTO @t
                SELECT  '呙' ,
                        'guo'; 
        INSERT  INTO @t
                SELECT  '妎' ,
                        'ha'; 
        INSERT  INTO @t
                SELECT  '咍' ,
                        'hai'; 
        INSERT  INTO @t
                SELECT  '乤' ,
                        'hal'; 
        INSERT  INTO @t
                SELECT  '兯' ,
                        'han'; 
        INSERT  INTO @t
                SELECT  '魧' ,
                        'hang'; 
        INSERT  INTO @t
                SELECT  '茠' ,
                        'hao'; 
        INSERT  INTO @t
                SELECT  '兞' ,
                        'haoke'; 
        INSERT  INTO @t
                SELECT  '诃' ,
                        'he'; 
        INSERT  INTO @t
                SELECT  '黒' ,
                        'hei'; 
        INSERT  INTO @t
                SELECT  '拫' ,
                        'hen'; 
        INSERT  INTO @t
                SELECT  '亨' ,
                        'heng'; 
        INSERT  INTO @t
                SELECT  '囍' ,
                        'heui'; 
        INSERT  INTO @t
                SELECT  '乊' ,
                        'ho'; 
        INSERT  INTO @t
                SELECT  '乥' ,
                        'hol'; 
        INSERT  INTO @t
                SELECT  '叿' ,
                        'hong'; 
        INSERT  INTO @t
                SELECT  '齁' ,
                        'hou'; 
        INSERT  INTO @t
                SELECT  '乎' ,
                        'hu'; 
        INSERT  INTO @t
                SELECT  '花' ,
                        'hua'; 
        INSERT  INTO @t
                SELECT  '徊' ,
                        'huai'; 
        INSERT  INTO @t
                SELECT  '欢' ,
                        'huan'; 
        INSERT  INTO @t
                SELECT  '巟' ,
                        'huang'; 
        INSERT  INTO @t
                SELECT  '灰' ,
                        'hui'; 
        INSERT  INTO @t
                SELECT  '昏' ,
                        'hun'; 
        INSERT  INTO @t
                SELECT  '吙' ,
                        'huo'; 
        INSERT  INTO @t
                SELECT  '嚿' ,
                        'geo'; 
        INSERT  INTO @t
                SELECT  '夻' ,
                        'hwa'; 
        INSERT  INTO @t
                SELECT  '丌' ,
                        'ji'; 
        INSERT  INTO @t
                SELECT  '加' ,
                        'jia'; 
        INSERT  INTO @t
                SELECT  '嗧' ,
                        'jialun'; 
        INSERT  INTO @t
                SELECT  '戋' ,
                        'jian'; 
        INSERT  INTO @t
                SELECT  '江' ,
                        'jiang'; 
        INSERT  INTO @t
                SELECT  '艽' ,
                        'jiao'; 
        INSERT  INTO @t
                SELECT  '阶' ,
                        'jie'; 
        INSERT  INTO @t
                SELECT  '巾' ,
                        'jin'; 
        INSERT  INTO @t
                SELECT  '坕' ,
                        'jing'; 
        INSERT  INTO @t
                SELECT  '冂' ,
                        'jiong'; 
        INSERT  INTO @t
                SELECT  '丩' ,
                        'jiu'; 
        INSERT  INTO @t
                SELECT  '欍' ,
                        'jou'; 
        INSERT  INTO @t
                SELECT  '凥' ,
                        'ju'; 
        INSERT  INTO @t
                SELECT  '姢' ,
                        'juan'; 
        INSERT  INTO @t
                SELECT  '噘' ,
                        'jue'; 
        INSERT  INTO @t
                SELECT  '军' ,
                        'jun'; 
        INSERT  INTO @t
                SELECT  '咔' ,
                        'ka'; 
        INSERT  INTO @t
                SELECT  '开' ,
                        'kai'; 
        INSERT  INTO @t
                SELECT  '乫' ,
                        'kal'; 
        INSERT  INTO @t
                SELECT  '刊' ,
                        'kan'; 
        INSERT  INTO @t
                SELECT  '冚' ,
                        'hem'; 
        INSERT  INTO @t
                SELECT  '砊' ,
                        'kang'; 
        INSERT  INTO @t
                SELECT  '尻' ,
                        'kao'; 
        INSERT  INTO @t
                SELECT  '坷' ,
                        'ke'; 
        INSERT  INTO @t
                SELECT  '肎' ,
                        'ken'; 
        INSERT  INTO @t
                SELECT  '劥' ,
                        'keng'; 
        INSERT  INTO @t
                SELECT  '巪' ,
                        'keo'; 
        INSERT  INTO @t
                SELECT  '乬' ,
                        'keol'; 
        INSERT  INTO @t
                SELECT  '唟' ,
                        'keos'; 
        INSERT  INTO @t
                SELECT  '厼' ,
                        'keum'; 
        INSERT  INTO @t
                SELECT  '怾' ,
                        'ki'; 
        INSERT  INTO @t
                SELECT  '空' ,
                        'kong'; 
        INSERT  INTO @t
                SELECT  '廤' ,
                        'kos'; 
        INSERT  INTO @t
                SELECT  '抠' ,
                        'kou'; 
        INSERT  INTO @t
                SELECT  '扝' ,
                        'ku'; 
        INSERT  INTO @t
                SELECT  '夸' ,
                        'kua'; 
        INSERT  INTO @t
                SELECT  '蒯' ,
                        'kuai'; 
        INSERT  INTO @t
                SELECT  '宽' ,
                        'kuan'; 
        INSERT  INTO @t
                SELECT  '匡' ,
                        'kuang'; 
        INSERT  INTO @t
                SELECT  '亏' ,
                        'kui'; 
        INSERT  INTO @t
                SELECT  '坤' ,
                        'kun'; 
        INSERT  INTO @t
                SELECT  '拡' ,
                        'kuo'; 
        INSERT  INTO @t
                SELECT  '穒' ,
                        'kweok'; 
        INSERT  INTO @t
                SELECT  '垃' ,
                        'la'; 
        INSERT  INTO @t
                SELECT  '来' ,
                        'lai'; 
        INSERT  INTO @t
                SELECT  '兰' ,
                        'lan'; 
        INSERT  INTO @t
                SELECT  '啷' ,
                        'lang'; 
        INSERT  INTO @t
                SELECT  '捞' ,
                        'lao'; 
        INSERT  INTO @t
                SELECT  '仂' ,
                        'le'; 
        INSERT  INTO @t
                SELECT  '雷' ,
                        'lei'; 
        INSERT  INTO @t
                SELECT  '塄' ,
                        'leng'; 
        INSERT  INTO @t
                SELECT  '唎' ,
                        'li'; 
        INSERT  INTO @t
                SELECT  '俩' ,
                        'lia'; 
        INSERT  INTO @t
                SELECT  '嫾' ,
                        'lian'; 
        INSERT  INTO @t
                SELECT  '簗' ,
                        'liang'; 
        INSERT  INTO @t
                SELECT  '蹽' ,
                        'liao'; 
        INSERT  INTO @t
                SELECT  '毟' ,
                        'lie'; 
        INSERT  INTO @t
                SELECT  '厸' ,
                        'lin'; 
        INSERT  INTO @t
                SELECT  '伶' ,
                        'ling'; 
        INSERT  INTO @t
                SELECT  '溜' ,
                        'liu'; 
        INSERT  INTO @t
                SELECT  '瓼' ,
                        'liwa'; 
        INSERT  INTO @t
                SELECT  '囖' ,
                        'lo'; 
        INSERT  INTO @t
                SELECT  '龙' ,
                        'long'; 
        INSERT  INTO @t
                SELECT  '娄' ,
                        'lou'; 
        INSERT  INTO @t
                SELECT  '噜' ,
                        'lu'; 
        INSERT  INTO @t
                SELECT  '驴' ,
                        'lv'; 
        INSERT  INTO @t
                SELECT  '寽' ,
                        'lue'; 
        INSERT  INTO @t
                SELECT  '孪' ,
                        'luan'; 
        INSERT  INTO @t
                SELECT  '掄' ,
                        'lun'; 
        INSERT  INTO @t
                SELECT  '頱' ,
                        'luo'; 
        INSERT  INTO @t
                SELECT  '呣' ,
                        'm'; 
        INSERT  INTO @t
                SELECT  '妈' ,
                        'ma'; 
        INSERT  INTO @t
                SELECT  '遤' ,
                        'hweong'; 
        INSERT  INTO @t
                SELECT  '埋' ,
                        'mai'; 
        INSERT  INTO @t
                SELECT  '颟' ,
                        'man'; 
        INSERT  INTO @t
                SELECT  '牤' ,
                        'mang'; 
        INSERT  INTO @t
                SELECT  '匁' ,
                        'mangmi'; 
        INSERT  INTO @t
                SELECT  '猫' ,
                        'mao'; 
        INSERT  INTO @t
                SELECT  '唜' ,
                        'mas'; 
        INSERT  INTO @t
                SELECT  '庅' ,
                        'me'; 
        INSERT  INTO @t
                SELECT  '呅' ,
                        'mei'; 
        INSERT  INTO @t
                SELECT  '椚' ,
                        'men'; 
        INSERT  INTO @t
                SELECT  '掹' ,
                        'meng'; 
        INSERT  INTO @t
                SELECT  '踎' ,
                        'meo'; 
        INSERT  INTO @t
                SELECT  '瞇' ,
                        'mi'; 
        INSERT  INTO @t
                SELECT  '宀' ,
                        'mian'; 
        INSERT  INTO @t
                SELECT  '喵' ,
                        'miao'; 
        INSERT  INTO @t
                SELECT  '乜' ,
                        'mie'; 
        INSERT  INTO @t
                SELECT  '瓱' ,
                        'miliklanm'; 
        INSERT  INTO @t
                SELECT  '民' ,
                        'min'; 
        INSERT  INTO @t
                SELECT  '冧' ,
                        'lem'; 
        INSERT  INTO @t
                SELECT  '名' ,
                        'ming'; 
        INSERT  INTO @t
                SELECT  '谬' ,
                        'miu'; 
        INSERT  INTO @t
                SELECT  '摸' ,
                        'mo'; 
        INSERT  INTO @t
                SELECT  '乮' ,
                        'mol'; 
        INSERT  INTO @t
                SELECT  '哞' ,
                        'mou'; 
        INSERT  INTO @t
                SELECT  '母' ,
                        'mu'; 
        INSERT  INTO @t
                SELECT  '旀' ,
                        'myeo'; 
        INSERT  INTO @t
                SELECT  '丆' ,
                        'myeon'; 
        INSERT  INTO @t
                SELECT  '椧' ,
                        'myeong'; 
        INSERT  INTO @t
                SELECT  '拏' ,
                        'na'; 
        INSERT  INTO @t
                SELECT  '腉' ,
                        'nai'; 
        INSERT  INTO @t
                SELECT  '囡' ,
                        'nan'; 
        INSERT  INTO @t
                SELECT  '囔' ,
                        'nang'; 
        INSERT  INTO @t
                SELECT  '乪' ,
                        'keg'; 
        INSERT  INTO @t
                SELECT  '孬' ,
                        'nao'; 
        INSERT  INTO @t
                SELECT  '疒' ,
                        'ne'; 
        INSERT  INTO @t
                SELECT  '娞' ,
                        'nei'; 
        INSERT  INTO @t
                SELECT  '焾' ,
                        'nem'; 
        INSERT  INTO @t
                SELECT  '嫩' ,
                        'nen'; 
        INSERT  INTO @t
                SELECT  '莻' ,
                        'neus'; 
        INSERT  INTO @t
                SELECT  '鈪' ,
                        'ngag'; 
        INSERT  INTO @t
                SELECT  '銰' ,
                        'ngai'; 
        INSERT  INTO @t
                SELECT  '啱' ,
                        'ngam'; 
        INSERT  INTO @t
                SELECT  '妮' ,
                        'ni'; 
        INSERT  INTO @t
                SELECT  '年' ,
                        'nian'; 
        INSERT  INTO @t
                SELECT  '娘' ,
                        'niang'; 
        INSERT  INTO @t
                SELECT  '茑' ,
                        'niao'; 
        INSERT  INTO @t
                SELECT  '捏' ,
                        'nie'; 
        INSERT  INTO @t
                SELECT  '脌' ,
                        'nin'; 
        INSERT  INTO @t
                SELECT  '宁' ,
                        'ning'; 
        INSERT  INTO @t
                SELECT  '牛' ,
                        'niu'; 
        INSERT  INTO @t
                SELECT  '农' ,
                        'nong'; 
        INSERT  INTO @t
                SELECT  '羺' ,
                        'nou'; 
        INSERT  INTO @t
                SELECT  '奴' ,
                        'nu'; 
        INSERT  INTO @t
                SELECT  '女' ,
                        'nv'; 
        INSERT  INTO @t
                SELECT  '疟' ,
                        'nue'; 
        INSERT  INTO @t
                SELECT  '瘧' ,
                        'nve'; 
        INSERT  INTO @t
                SELECT  '奻' ,
                        'nuan'; 
        INSERT  INTO @t
                SELECT  '黁' ,
                        'nun'; 
        INSERT  INTO @t
                SELECT  '燶' ,
                        'nung'; 
        INSERT  INTO @t
                SELECT  '挪' ,
                        'nuo'; 
        INSERT  INTO @t
                SELECT  '筽' ,
                        'o'; 
        INSERT  INTO @t
                SELECT  '夞' ,
                        'oes'; 
        INSERT  INTO @t
                SELECT  '乯' ,
                        'ol'; 
        INSERT  INTO @t
                SELECT  '鞰' ,
                        'on'; 
        INSERT  INTO @t
                SELECT  '讴' ,
                        'ou'; 
        INSERT  INTO @t
                SELECT  '妑' ,
                        'pa'; 
        INSERT  INTO @t
                SELECT  '俳' ,
                        'pai'; 
        INSERT  INTO @t
                SELECT  '磗' ,
                        'pak'; 
        INSERT  INTO @t
                SELECT  '眅' ,
                        'pan'; 
        INSERT  INTO @t
                SELECT  '乓' ,
                        'pang'; 
        INSERT  INTO @t
                SELECT  '抛' ,
                        'pao'; 
        INSERT  INTO @t
                SELECT  '呸' ,
                        'pei'; 
        INSERT  INTO @t
                SELECT  '瓫' ,
                        'pen'; 
        INSERT  INTO @t
                SELECT  '匉' ,
                        'peng'; 
        INSERT  INTO @t
                SELECT  '浌' ,
                        'peol'; 
        INSERT  INTO @t
                SELECT  '巼' ,
                        'phas'; 
        INSERT  INTO @t
                SELECT  '闏' ,
                        'phdeng'; 
        INSERT  INTO @t
                SELECT  '乶' ,
                        'phoi'; 
        INSERT  INTO @t
                SELECT  '喸' ,
                        'phos'; 
        INSERT  INTO @t
                SELECT  '丕' ,
                        'pi'; 
        INSERT  INTO @t
                SELECT  '囨' ,
                        'pian'; 
        INSERT  INTO @t
                SELECT  '缥' ,
                        'piao'; 
        INSERT  INTO @t
                SELECT  '氕' ,
                        'pie'; 
        INSERT  INTO @t
                SELECT  '丿' ,
                        'pianpang'; 
        INSERT  INTO @t
                SELECT  '姘' ,
                        'pin'; 
        INSERT  INTO @t
                SELECT  '乒' ,
                        'ping'; 
        INSERT  INTO @t
                SELECT  '钋' ,
                        'po'; 
        INSERT  INTO @t
                SELECT  '剖' ,
                        'pou'; 
        INSERT  INTO @t
                SELECT  '哣' ,
                        'deo'; 
        INSERT  INTO @t
                SELECT  '兺' ,
                        'ppun'; 
        INSERT  INTO @t
                SELECT  '仆' ,
                        'pu'; 
        INSERT  INTO @t
                SELECT  '七' ,
                        'qi'; 
        INSERT  INTO @t
                SELECT  '掐' ,
                        'qia'; 
        INSERT  INTO @t
                SELECT  '千' ,
                        'qian'; 
        INSERT  INTO @t
                SELECT  '羌' ,
                        'qiang'; 
        INSERT  INTO @t
                SELECT  '兛' ,
                        'qianke'; 
        INSERT  INTO @t
                SELECT  '瓩' ,
                        'qianwa'; 
        INSERT  INTO @t
                SELECT  '悄' ,
                        'qiao'; 
        INSERT  INTO @t
                SELECT  '苆' ,
                        'qie'; 
        INSERT  INTO @t
                SELECT  '亲' ,
                        'qin'; 
        INSERT  INTO @t
                SELECT  '蠄' ,
                        'kem'; 
        INSERT  INTO @t
                SELECT  '氢' ,
                        'qing'; 
        INSERT  INTO @t
                SELECT  '銎' ,
                        'qiong'; 
        INSERT  INTO @t
                SELECT  '丘' ,
                        'qiu'; 
        INSERT  INTO @t
                SELECT  '曲' ,
                        'qu'; 
        INSERT  INTO @t
                SELECT  '迲' ,
                        'keop'; 
        INSERT  INTO @t
                SELECT  '峑' ,
                        'quan'; 
        INSERT  INTO @t
                SELECT  '蒛' ,
                        'que'; 
        INSERT  INTO @t
                SELECT  '夋' ,
                        'qun'; 
        INSERT  INTO @t
                SELECT  '亽' ,
                        'ra'; 
        INSERT  INTO @t
                SELECT  '囕' ,
                        'ram'; 
        INSERT  INTO @t
                SELECT  '呥' ,
                        'ran'; 
        INSERT  INTO @t
                SELECT  '穣' ,
                        'rang'; 
        INSERT  INTO @t
                SELECT  '荛' ,
                        'rao'; 
        INSERT  INTO @t
                SELECT  '惹' ,
                        're'; 
        INSERT  INTO @t
                SELECT  '人' ,
                        'ren'; 
        INSERT  INTO @t
                SELECT  '扔' ,
                        'reng'; 
        INSERT  INTO @t
                SELECT  '日' ,
                        'ri'; 
        INSERT  INTO @t
                SELECT  '栄' ,
                        'rong'; 
        INSERT  INTO @t
                SELECT  '禸' ,
                        'rou'; 
        INSERT  INTO @t
                SELECT  '嶿' ,
                        'ru'; 
        INSERT  INTO @t
                SELECT  '撋' ,
                        'ruan'; 
        INSERT  INTO @t
                SELECT  '桵' ,
                        'rui'; 
        INSERT  INTO @t
                SELECT  '闰' ,
                        'run'; 
        INSERT  INTO @t
                SELECT  '叒' ,
                        'ruo'; 
        INSERT  INTO @t
                SELECT  '仨' ,
                        'sa'; 
        INSERT  INTO @t
                SELECT  '栍' ,
                        'saeng'; 
        INSERT  INTO @t
                SELECT  '毢' ,
                        'sai'; 
        INSERT  INTO @t
                SELECT  '虄' ,
                        'sal'; 
        INSERT  INTO @t
                SELECT  '三' ,
                        'san'; 
        INSERT  INTO @t
                SELECT  '桒' ,
                        'sang'; 
        INSERT  INTO @t
                SELECT  '掻' ,
                        'sao'; 
        INSERT  INTO @t
                SELECT  '色' ,
                        'se'; 
        INSERT  INTO @t
                SELECT  '裇' ,
                        'sed'; 
        INSERT  INTO @t
                SELECT  '聓' ,
                        'sei'; 
        INSERT  INTO @t
                SELECT  '森' ,
                        'sen'; 
        INSERT  INTO @t
                SELECT  '鬙' ,
                        'seng'; 
        INSERT  INTO @t
                SELECT  '閪' ,
                        'seo'; 
        INSERT  INTO @t
                SELECT  '縇' ,
                        'seon'; 
        INSERT  INTO @t
                SELECT  '杀' ,
                        'sha'; 
        INSERT  INTO @t
                SELECT  '筛' ,
                        'shai'; 
        INSERT  INTO @t
                SELECT  '山' ,
                        'shan'; 
        INSERT  INTO @t
                SELECT  '伤' ,
                        'shang'; 
        INSERT  INTO @t
                SELECT  '弰' ,
                        'shao'; 
        INSERT  INTO @t
                SELECT  '奢' ,
                        'she'; 
        INSERT  INTO @t
                SELECT  '申' ,
                        'shen'; 
        INSERT  INTO @t
                SELECT  '升' ,
                        'sheng'; 
        INSERT  INTO @t
                SELECT  '尸' ,
                        'shi'; 
        INSERT  INTO @t
                SELECT  '兙' ,
                        'shike'; 
        INSERT  INTO @t
                SELECT  '瓧' ,
                        'shiwa'; 
        INSERT  INTO @t
                SELECT  '収' ,
                        'shou'; 
        INSERT  INTO @t
                SELECT  '书' ,
                        'shu'; 
        INSERT  INTO @t
                SELECT  '刷' ,
                        'shua'; 
        INSERT  INTO @t
                SELECT  '摔' ,
                        'shuai'; 
        INSERT  INTO @t
                SELECT  '闩' ,
                        'shuan'; 
        INSERT  INTO @t
                SELECT  '双' ,
                        'shuang'; 
        INSERT  INTO @t
                SELECT  '谁' ,
                        'shei'; 
        INSERT  INTO @t
                SELECT  '脽' ,
                        'shui'; 
        INSERT  INTO @t
                SELECT  '吮' ,
                        'shun'; 
        INSERT  INTO @t
                SELECT  '哾' ,
                        'shuo'; 
        INSERT  INTO @t
                SELECT  '丝' ,
                        'si'; 
        INSERT  INTO @t
                SELECT  '螦' ,
                        'so'; 
        INSERT  INTO @t
                SELECT  '乺' ,
                        'sol'; 
        INSERT  INTO @t
                SELECT  '忪' ,
                        'song'; 
        INSERT  INTO @t
                SELECT  '凁' ,
                        'sou'; 
        INSERT  INTO @t
                SELECT  '苏' ,
                        'su'; 
        INSERT  INTO @t
                SELECT  '痠' ,
                        'suan'; 
        INSERT  INTO @t
                SELECT  '夊' ,
                        'sui'; 
        INSERT  INTO @t
                SELECT  '孙' ,
                        'sun'; 
        INSERT  INTO @t
                SELECT  '娑' ,
                        'suo'; 
        INSERT  INTO @t
                SELECT  '他' ,
                        'ta'; 
        INSERT  INTO @t
                SELECT  '襨' ,
                        'tae'; 
        INSERT  INTO @t
                SELECT  '囼' ,
                        'tai'; 
        INSERT  INTO @t
                SELECT  '坍' ,
                        'tan'; 
        INSERT  INTO @t
                SELECT  '铴' ,
                        'tang'; 
        INSERT  INTO @t
                SELECT  '仐' ,
                        'tao'; 
        INSERT  INTO @t
                SELECT  '畓' ,
                        'tap'; 
        INSERT  INTO @t
                SELECT  '忒' ,
                        'te'; 
        INSERT  INTO @t
                SELECT  '膯' ,
                        'teng'; 
        INSERT  INTO @t
                SELECT  '唞' ,
                        'teo'; 
        INSERT  INTO @t
                SELECT  '朰' ,
                        'teul'; 
        INSERT  INTO @t
                SELECT  '剔' ,
                        'ti'; 
        INSERT  INTO @t
                SELECT  '天' ,
                        'tian'; 
        INSERT  INTO @t
                SELECT  '旫' ,
                        'tiao'; 
        INSERT  INTO @t
                SELECT  '怗' ,
                        'tie'; 
        INSERT  INTO @t
                SELECT  '厅' ,
                        'ting'; 
        INSERT  INTO @t
                SELECT  '乭' ,
                        'tol'; 
        INSERT  INTO @t
                SELECT  '囲' ,
                        'tong'; 
        INSERT  INTO @t
                SELECT  '偷' ,
                        'tou'; 
        INSERT  INTO @t
                SELECT  '凸' ,
                        'tu'; 
        INSERT  INTO @t
                SELECT  '湍' ,
                        'tuan'; 
        INSERT  INTO @t
                SELECT  '推' ,
                        'tui'; 
        INSERT  INTO @t
                SELECT  '旽' ,
                        'tun'; 
        INSERT  INTO @t
                SELECT  '乇' ,
                        'tuo'; 
        INSERT  INTO @t
                SELECT  '屲' ,
                        'wa'; 
        INSERT  INTO @t
                SELECT  '歪' ,
                        'wai'; 
        INSERT  INTO @t
                SELECT  '乛' ,
                        'wan'; 
        INSERT  INTO @t
                SELECT  '尣' ,
                        'wang'; 
        INSERT  INTO @t
                SELECT  '危' ,
                        'wei'; 
        INSERT  INTO @t
                SELECT  '塭' ,
                        'wen'; 
        INSERT  INTO @t
                SELECT  '翁' ,
                        'weng'; 
        INSERT  INTO @t
                SELECT  '挝' ,
                        'wo'; 
        INSERT  INTO @t
                SELECT  '乌' ,
                        'wu'; 
        INSERT  INTO @t
                SELECT  '夕' ,
                        'xi'; 
        INSERT  INTO @t
                SELECT  '诶' ,
                        'ei'; 
        INSERT  INTO @t
                SELECT  '疨' ,
                        'xia'; 
        INSERT  INTO @t
                SELECT  '仙' ,
                        'xian'; 
        INSERT  INTO @t
                SELECT  '乡' ,
                        'xiang'; 
        INSERT  INTO @t
                SELECT  '灱' ,
                        'xiao'; 
        INSERT  INTO @t
                SELECT  '楔' ,
                        'xie'; 
        INSERT  INTO @t
                SELECT  '心' ,
                        'xin'; 
        INSERT  INTO @t
                SELECT  '星' ,
                        'xing'; 
        INSERT  INTO @t
                SELECT  '凶' ,
                        'xiong'; 
        INSERT  INTO @t
                SELECT  '休' ,
                        'xiu'; 
        INSERT  INTO @t
                SELECT  '旴' ,
                        'xu'; 
        INSERT  INTO @t
                SELECT  '昍' ,
                        'xuan'; 
        INSERT  INTO @t
                SELECT  '疶' ,
                        'xue'; 
        INSERT  INTO @t
                SELECT  '坃' ,
                        'xun'; 
        INSERT  INTO @t
                SELECT  '丫' ,
                        'ya'; 
        INSERT  INTO @t
                SELECT  '咽' ,
                        'yan'; 
        INSERT  INTO @t
                SELECT  '欕' ,
                        'eom'; 
        INSERT  INTO @t
                SELECT  '央' ,
                        'yang'; 
        INSERT  INTO @t
                SELECT  '吆' ,
                        'yao'; 
        INSERT  INTO @t
                SELECT  '椰' ,
                        'ye'; 
        INSERT  INTO @t
                SELECT  '膶' ,
                        'yen'; 
        INSERT  INTO @t
                SELECT  '一' ,
                        'yi'; 
        INSERT  INTO @t
                SELECT  '乁' ,
                        'i'; 
        INSERT  INTO @t
                SELECT  '乚' ,
                        'yin'; 
        INSERT  INTO @t
                SELECT  '应' ,
                        'ying'; 
        INSERT  INTO @t
                SELECT  '哟' ,
                        'yo'; 
        INSERT  INTO @t
                SELECT  '佣' ,
                        'yong'; 
        INSERT  INTO @t
                SELECT  '优' ,
                        'you'; 
        INSERT  INTO @t
                SELECT  '迂' ,
                        'yu'; 
        INSERT  INTO @t
                SELECT  '囦' ,
                        'yuan'; 
        INSERT  INTO @t
                SELECT  '曰' ,
                        'yue'; 
        INSERT  INTO @t
                SELECT  '蒀' ,
                        'yun'; 
        INSERT  INTO @t
                SELECT  '帀' ,
                        'za'; 
        INSERT  INTO @t
                SELECT  '災' ,
                        'zai'; 
        INSERT  INTO @t
                SELECT  '兂' ,
                        'zan'; 
        INSERT  INTO @t
                SELECT  '牂' ,
                        'zang'; 
        INSERT  INTO @t
                SELECT  '遭' ,
                        'zao'; 
        INSERT  INTO @t
                SELECT  '啫' ,
                        'ze'; 
        INSERT  INTO @t
                SELECT  '贼' ,
                        'zei'; 
        INSERT  INTO @t
                SELECT  '怎' ,
                        'zen'; 
        INSERT  INTO @t
                SELECT  '曽' ,
                        'zeng'; 
        INSERT  INTO @t
                SELECT  '吒' ,
                        'zha'; 
        INSERT  INTO @t
                SELECT  '甴' ,
                        'gad'; 
        INSERT  INTO @t
                SELECT  '夈' ,
                        'zhai'; 
        INSERT  INTO @t
                SELECT  '毡' ,
                        'zhan'; 
        INSERT  INTO @t
                SELECT  '张' ,
                        'zhang'; 
        INSERT  INTO @t
                SELECT  '钊' ,
                        'zhao'; 
        INSERT  INTO @t
                SELECT  '蜇' ,
                        'zhe'; 
        INSERT  INTO @t
                SELECT  '贞' ,
                        'zhen'; 
        INSERT  INTO @t
                SELECT  '凧' ,
                        'zheng'; 
        INSERT  INTO @t
                SELECT  '之' ,
                        'zhi'; 
        INSERT  INTO @t
                SELECT  '中' ,
                        'zhong'; 
        INSERT  INTO @t
                SELECT  '州' ,
                        'zhou'; 
        INSERT  INTO @t
                SELECT  '劯' ,
                        'zhu'; 
        INSERT  INTO @t
                SELECT  '抓' ,
                        'zhua'; 
        INSERT  INTO @t
                SELECT  '专' ,
                        'zhuan'; 
        INSERT  INTO @t
                SELECT  '转' ,
                        'zhuai'; 
        INSERT  INTO @t
                SELECT  '妆' ,
                        'zhuang'; 
        INSERT  INTO @t
                SELECT  '骓' ,
                        'zhui'; 
        INSERT  INTO @t
                SELECT  '宒' ,
                        'zhun'; 
        INSERT  INTO @t
                SELECT  '卓' ,
                        'zhuo'; 
        INSERT  INTO @t
                SELECT  '孜' ,
                        'zi'; 
        INSERT  INTO @t
                SELECT  '唨' ,
                        'zo'; 
        INSERT  INTO @t
                SELECT  '宗' ,
                        'zong'; 
        INSERT  INTO @t
                SELECT  '棸' ,
                        'zou'; 
        INSERT  INTO @t
                SELECT  '哫' ,
                        'zu'; 
        INSERT  INTO @t
                SELECT  '劗' ,
                        'zuan'; 
        INSERT  INTO @t
                SELECT  '厜' ,
                        'zui'; 
        INSERT  INTO @t
                SELECT  '尊' ,
                        'zun'; 
        INSERT  INTO @t
                SELECT  '昨' ,
                        'zuo'; 
 
        DECLARE @strlen INT; 
        SELECT  @strlen = LEN(@str) ,
                @re = '';
        WHILE @strlen > 0
            BEGIN     
                SELECT TOP 1
                        @re = UPPER(SUBSTRING(py, 1, 1)) + SUBSTRING(py, 2,
                                                              LEN(py)) + @re ,
                        @strlen = @strlen - 1
                FROM    @t a
                WHERE   chr <= SUBSTRING(@str, @strlen, 1)
                ORDER BY chr COLLATE Chinese_PRC_CS_AS_KS_WS DESC; 
                IF @@rowcount = 0
                    SELECT  @re = SUBSTRING(@str, @strlen, 1) + @re ,
                            @strlen = @strlen - 1;
            END;
        RETURN(@re);
    END;