﻿insert into tbToolDetail (ToolID,DetailIcon,IconName,Name,NameJP,NameQP,Description,CreateUser,CreateTime,UpdateUser,UpdateTime,SortOrder,Enabled) select ToolID,0,'','','','','','system',GETDATE(),'system',GETDATE(),,'1' from tbTool where Name=''
