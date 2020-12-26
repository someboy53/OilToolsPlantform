/****** Object:  UserDefinedFunction [dbo].[fn_GetPy]    Script Date: 01/04/2017 11:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--生成拼音首码  
ALTER  FUNCTION [dbo].[fn_getPy] ( @str NVARCHAR(4000) )
RETURNS NVARCHAR(4000)  
  --WITH  ENCRYPTION  
AS
    BEGIN  
        DECLARE @intLen INT;  
        DECLARE @strRet NVARCHAR(4000);  
        DECLARE @temp NVARCHAR(100);  
        SET @intLen = LEN(@str);  
        SET @strRet = '';  
        WHILE @intLen > 0
            BEGIN  
                SET @temp = '';  
                SELECT  @temp = CASE WHEN SUBSTRING(@str, @intLen, 1) >= '帀'
                                     THEN 'Z'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '丫'
                                     THEN 'Y'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '夕'
                                     THEN 'X'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '屲'
                                     THEN 'W'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '他'
                                     THEN 'T'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '仨'
                                     THEN 'S'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '呥'
                                     THEN 'R'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '七'
                                     THEN 'Q'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '妑'
                                     THEN 'P'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '噢'
                                     THEN 'O'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '拏'
                                     THEN 'N'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '嘸'
                                     THEN 'M'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '垃'
                                     THEN 'L'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '咔'
                                     THEN 'K'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '丌'
                                     THEN 'J'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '铪'
                                     THEN 'H'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '旮'
                                     THEN 'G'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '发'
                                     THEN 'F'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '妸'
                                     THEN 'E'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '咑'
                                     THEN 'D'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '嚓'
                                     THEN 'C'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '八'
                                     THEN 'B'
                                     WHEN SUBSTRING(@str, @intLen, 1) >= '吖'
                                     THEN 'A'
                                     ELSE RTRIM(LTRIM(SUBSTRING(@str, @intLen,
                                                              1)))
                                END;  
  --对于汉字特殊字符，不生成拼音码  
                IF ( ASCII(@temp) > 127 )
                    SET @temp = '';  
  --对于英文中小括号，不生成拼音码  
                IF @temp = '('
                    OR @temp = ')'
                    SET @temp = '';  
                SELECT  @strRet = @temp + @strRet;  
                SET @intLen = @intLen - 1;  
            END;  
        RETURN  LOWER(@strRet);  
    END;  