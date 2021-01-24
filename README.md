# Hzdtf.Foundation.FrameworkV5
基础框架系统V5，升级到.Net5版本。具体用法请参考Hzdtf.Foundation.Framework项目

优化点：
1、实体主键类型支持多种
（1）、整型（自增）
（2）、字符串型
（3）、Guid型
（4）、雪花算法（长整型）

2、支持SAAS应用，实体主要以租户ID为标识，持久化层自动将租户ID过滤当前用户的租户ID。
3、判断菜单功能权限不再使用AOP，改为使用中间件实现。
4、SQL Server改为支持2012版本以上。
