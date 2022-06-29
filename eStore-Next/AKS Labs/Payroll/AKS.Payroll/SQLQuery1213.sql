--select A.VendorId , VendorName from  V1_PurchaseProducts as A Inner Join V1_Vendors  AS B On
--B.VendorId=A.VendorId
--Group By A.VendorId , VendorName 
--select * from V1_Vendors

select *,B.VendorId from  V1_PurchaseItems As A Inner Join 
V1_PurchaseProducts As B On B.InwardNumber=A.InwardNumber
where B.VendorId='1'
select * from V1_Vendors