# CUBETestAPI

CUBE .NET Engineer線上作業實作

## 功能簡述與API對照

1. GET /api/BitcoinPrice/current: 將coindesk API轉換資料並重新輸出
2. GET /api/CurrencyNameMapping: 查詢全部幣別中文名稱對照表
3. GET /api/CurrnecyNameMapping/{id}: 查詢該id之幣別中文名稱對照表
4. POST /api/CurrencyNameMapping: 新增幣別中文名稱對照表資料
5. PUT /api/CurrencyNameMapping/{id}: 修改該id之幣別中文名稱對照表
6. DELETE /api/CurrencyNameMapping/{id}: 刪除該id之幣別中文名稱對照表

## 主要程式功能簡述
1. Controller/BitcoinPriceController: coindesk API處理轉換API
2. Controller/CurrencyNameMappingController: 幣別中文名稱CRUD API
3. Repository: Database CRUD
4. CUBETestAPITest: 各API與DB CRUD Unit Test

## Database Table Schema

CREATE TABLE CurrencyNameMapping(ID UNIQUEIDENTIFIER NOT NULL,
							     Currency NVARCHAR(100) NULL,
								 ChineseName NVARCHAR(100) NULL,
								 PRIMARY KEY CLUSTERED (ID ASC));

ID: 幣別ID(PK)
Currency: 幣別代號
ChineseName: 幣別中文名稱


## 已實作加分項目
1. 印出所有 API 被呼叫以及呼叫外部 API 的 request and response body log
2. Error handling 處理 API response
3. swagger-ui
4. design pattern 實作(MVC)
5. 能夠運行在 Docker
6. 加解密技術應用 (AES/RSA…etc.)

## Demo Youtube
(https://youtu.be/j7tiq78el8s?si=q_5FWWGTwgPw9Rio)
