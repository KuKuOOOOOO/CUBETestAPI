# CUBETestAPI

CUBE .NET Engineer�u�W�@�~��@

## �\��²�z�PAPI���

1. GET /api/BitcoinPrice/current: �Ncoindesk API�ഫ��ƨí��s��X
2. GET /api/CurrencyNameMapping: �d�ߥ������O����W�ٹ�Ӫ�
3. GET /api/CurrnecyNameMapping/{id}: �d�߸�id�����O����W�ٹ�Ӫ�
4. POST /api/CurrencyNameMapping: �s�W���O����W�ٹ�Ӫ���
5. PUT /api/CurrencyNameMapping/{id}: �ק��id�����O����W�ٹ�Ӫ�
6. DELETE /api/CurrencyNameMapping/{id}: �R����id�����O����W�ٹ�Ӫ�

## �D�n�{���\��²�z
1. Controller/BitcoinPriceController: coindesk API�B�z�ഫAPI
2. Controller/CurrencyNameMappingController: ���O����W��CRUD API
3. Repository: Database CRUD
4. CUBETestAPITest: �UAPI�PDB CRUD Unit Test

## Database Table Schema

CREATE TABLE CurrencyNameMapping(ID UNIQUEIDENTIFIER NOT NULL,
							     Currency NVARCHAR(100) NULL,
								 ChineseName NVARCHAR(100) NULL,
								 PRIMARY KEY CLUSTERED (ID ASC));

ID: ���OID(PK)
Currency: ���O�N��
ChineseName: ���O����W��


## �w��@�[������
1. �L�X�Ҧ� API �Q�I�s�H�ΩI�s�~�� API �� request and response body log
2. Error handling �B�z API response
3. swagger-ui
4. design pattern ��@(MVC)
5. ����B��b Docker

## Demo Youtube
![Demo](https://youtu.be/j7tiq78el8s?si=q_5FWWGTwgPw9Rio)