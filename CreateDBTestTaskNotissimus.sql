CREATE DATABASE TestTaskNotissimus;
GO

USE TestTaskNotissimus
CREATE TABLE Offers(
Id INT PRIMARY KEY,
Type NVARCHAR(50),
Url NVARCHAR(255),
Price INT,
CurrencyId NVARCHAR(50),
CategoryId INT,
Picture NVARCHAR(50),
Delivery BIT,
LocalDeliveryCost INT,
Description NVARCHAR(1000),
CustomFields xml);

GO
CREATE PROCEDURE AddOffer
    @XmlDocument XML
AS
BEGIN

INSERT INTO Offers(
Id,
Type,
Url,
Price,
CurrencyId,
CategoryId,
Picture,
Delivery,
LocalDeliveryCost,
Description,
CustomFields)

SELECT 
offer.value('@id','INT') AS Id,
offer.value('@type','NVARCHAR(50)') AS Type,
offer.value('(url/text())[1]','NVARCHAR(255)') AS Url,
offer.value('(price)[1]','INT') AS Price,
offer.value('(currencyId/text())[1]','NVARCHAR(50)') AS CurrencyId,
offer.value('(categoryId)[1]','INT') AS CategoryId,
offer.value('(picture/text())[1]','NVARCHAR(50)') AS Picture,
offer.value('(delivery)[1]','BIT') AS Delivery,
offer.value('(local_delivery_cost/text())[1]','INT') AS LocalDeliveryCost,
offer.value('(description/text())[1]','NVARCHAR(1000)') AS Description, 

@XmlDocument.query('<data>
    {
        for $x in //offers/offer[@id=sql:column("offer.OfferId")]/*[not(local-name(.) = ("url", "price", "currencyId", "categoryId", "picture", "delivery", "local_delivery_cost", "description"))]
        return $x
    }
    </data>') AS CustomFields 
FROM @XmlDocument.nodes('//offers/offer')AS TEMPTABLE(offer)
CROSS APPLY (SELECT TEMPTABLE.offer.value('@id','INT') AS OfferId) AS offer;
END

GO
CREATE PROCEDURE GetOffers AS
BEGIN
    SELECT *
    FROM Offers
END;