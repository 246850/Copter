# Copter - 一套基于.Net Framework 4.6.2应用程序框架

## 拥有特性
1、采用Autofac作为Ioc容器，致力于面向接口编程。  
2、可使用[Entityframework|Servicestack.Ormlite|Dapper]作为ORM，以仓储模式（IRepository<T>）方式来对数据库进行操作；若使用EF做为ORM，本框架采用的是CodeFirst方式，完美支持FluentMap映射。  
3、支持领域事件，通过发布（Publish）订阅(Subscirbe)模式，当领域模型状态改变做出相应操作，如更新缓存、发送短信/邮件等。  
4、IMapper接口，集成对象模型（如：领域实体转成业务模型）转换，可随意切换使用[AutoMapper|TinyMapper|EmitMapper|自己写的转换类库]等类库。
5、采用FluentValidation进行模型验证，使得Model和验证完美分离。#8194  
6、Copter还包含大量封装好的实用工具类库，如：队列，Cache，加解密，Excel导入导出，发送邮件，Http请求等。

## Copter即：直升机，寓意：直线上升，让我们一起来使用它吧！
参考资料，主要来自高质量开源B2C框架，其名曰：nopCommerce
