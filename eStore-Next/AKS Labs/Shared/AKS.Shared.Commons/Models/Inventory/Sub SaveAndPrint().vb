    Sub SaveAndPrint()
      
      If Worksheets("Invoice").Range("I5").Value > 0 And Worksheets("Invoice").Range("B10").Value <> " " Or Worksheets("Invoice").Range("B10").Value <> "" Then
      
      'Print Invoice
        Worksheets("Invoice").Range("J9").Value = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
         Worksheets("Invoice").Range("A1:F34").PrintOut From:=1, _
    To:=1, Copies:=2, Collate:=True
    
   
    ' Processing Data
        Dim LR As Long, LRS As Long
        Dim x As Integer
        Dim ws As Worksheet
        Dim costPrice As Long
        
        Set ws = Worksheets("InvoiceList")
        Set StockRange = Worksheets("StockList").Range("A1:J7000")
        
        'Getting Serial Nuber
        LR = ws.Cells(Rows.Count, 1).End(xlUp).Row + 1
        LRS = ws.Cells(Rows.Count, 15).End(xlUp).Row + 1
        costPrice = 0
        
        Dim tableName As ListObject, invoiceListTable, profitLossTable As ListObject
        
        ' Setting Table Name
        Set tableName = Worksheets("InvoiceList").ListObjects("InvoiceListTable")
        Set invoiceListTable = Worksheets("InvoiceList").ListObjects("InvoiceSummaryTable")
        Set profitLossTable = Worksheets("ProfitLoss").ListObjects("ProfitLossTable")
          
        Dim sumAddRow As ListRow
        Set sumAddRow = invoiceListTable.ListRows.Add()
       
       'Invoice Summary Table
        With sumAddRow
            .Range(1) = sumAddRow.Index
            .Range(2) = Worksheets("Invoice").Range("B28").Value
            .Range(3) = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
            .Range(4) = Replace(Worksheets("Invoice").Range("B6"), "Customer:", "")
            .Range(5) = Worksheets("Invoice").Range("E6").Value
            .Range(6) = Worksheets("Invoice").Range("C29").Value
            .Range(7) = Worksheets("Invoice").Range("i5").Value
        End With
        'End of Invoice Summary Table
        
        Dim addedRow As ListRow
        Set addedRow = tableName.ListRows.Add()
        
       ' Adding Invoice Detail with Bill Amount and Payment Mode # First Line 
        With addedRow
            .Range(1) = addedRow.Index
            .Range(2) = Worksheets("Invoice").Range("B28").Value
            .Range(3) = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
            .Range(4) = Replace(Worksheets("Invoice").Range("B6"), "Customer:", "")
            .Range(5) = Worksheets("Invoice").Range("E6").Value
            .Range(6) = Worksheets("Invoice").Range("B10").Value
            .Range(7) = Worksheets("Invoice").Range("C10").Value
            .Range(8) = Worksheets("Invoice").Range("D10").Value
            .Range(9) = Worksheets("Invoice").Range("E10").Value
            .Range(10) = Worksheets("Invoice").Range("F10").Value
            .Range(11) = Worksheets("Invoice").Range("i5").Value
            .Range(12) = Worksheets("Invoice").Range("C29").Value
            
        End With
        
         ' Adding ProfitLoss Data First Line
        Dim addProfitLossRow As ListRow
        Set addProfitLossRow = profitLossTable.ListRows.Add()
      
        costPrice = Application.WorksheetFunction.VLookup(Worksheets("Invoice").Range("B10").Value, StockRange, 2, False)
        
        'Date    No  Barcode   Quantity   MRP CostPrice Dis.Percentage DiscountAmount BasicPrice  TaxAmount   SalePrice     RoundOff    BilAmount   Profit/Loss
        With addProfitLossRow
        .Range(1) = Worksheets("Invoice").Range("B28").Value 'Date
        .Range(2) = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value 'InvoiceNo
        .Range(3) = Worksheets("Invoice").Range("B10").Value 'Barcode
        .Range(4) = Worksheets("Invoice").Range("D10").Value 'Qty
        .Range(5) = Worksheets("Invoice").Range("C10").Value 'MRP
        .Range(6) = costPrice 'CostPrice
        .Range(7) = Worksheets("Invoice").Range("E10").Value 'Discount %
        .Range(10) = Worksheets("Invoice").Range("F10").Value ' Amount
        .Range(12) = Worksheets("Invoice").Range("F25").Value ' RoundOff
        .Range(13) = Worksheets("Invoice").Range("I5").Value 'Bill Amount
         
        
        End With
        
         
        'Add More Row to List
        Dim RowN As Long
        RowN = 11
         For x = 11 To 24
         
         If Worksheets("Invoice").Cells(x, 2).Value = " " Or Worksheets("Invoice").Cells(x, 2).Value = "" And Worksheets("Invoice").Cells(x, 4).Value = " " Or Worksheets("Invoice").Cells(x, 4).Value = "" Then
            RowN = x + 1
         Else
         
         Dim addedRow2 As ListRow
         Set addedRow2 = tableName.ListRows.Add()
        
        
        With addedRow2
            .Range(1) = addedRow2.Index
            .Range(2) = Worksheets("Invoice").Range("B28").Value
            .Range(3) = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
            .Range(4) = Replace(Worksheets("Invoice").Range("B6"), "Customer:", "")
            .Range(5) = Worksheets("Invoice").Range("E6").Value
            .Range(6) = Worksheets("Invoice").Cells(x, 2).Value
            .Range(7) = Worksheets("Invoice").Cells(x, 3).Value
            .Range(8) = Worksheets("Invoice").Cells(x, 4).Value
            .Range(9) = Worksheets("Invoice").Cells(x, 5).Value
            .Range(10) = Worksheets("Invoice").Cells(x, 6).Value
            
            .Range(11) = 0
            .Range(12) = " "
        End With
        
        ' Adding ProfitLoss Data.
        'Dim addProfitLossRow As ListRow
       ' If Worksheets("Invoice").Cells(x, 2).Value <> "" Then
        Set addProfitLossRow = profitLossTable.ListRows.Add()
         MsgBox Worksheets("Invoice").Cells(x, 2).Value
        costPrice = Application.WorksheetFunction.VLookup(Worksheets("Invoice").Cells(x, 2).Value, StockRange, 2, False)
        
        'Date    No  Bar Code    Quantity    MRP Cost Price Dis.Percentage DiscountAmount  BasicPrice  TaxAmount   SalePrice     RoundOff    BilAmount   Profit/Loss
        With addProfitLossRow
        .Range(1) = Worksheets("Invoice").Range("B28").Value 'Date
        .Range(2) = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value 'InvoiceNo
        .Range(3) = Worksheets("Invoice").Cells(x, 2).Value 'Barcode
        .Range(4) = Worksheets("Invoice").Cells(x, 4).Value 'Qty
        .Range(5) = Worksheets("Invoice").Cells(x, 3).Value 'MRP
        .Range(6) = costPrice 'CostPrice
        .Range(7) = Worksheets("Invoice").Cells(x, 5).Value 'Discount %
        .Range(10) = Worksheets("Invoice").Cells(x, 6).Value ' Amount
        End With
        
       ' End If
        
          End If
         
        Next x
         Worksheets("Invoice").Range("B10:E24").Value = ""
          Worksheets("Invoice").Range("B6").Value = "Customer:  "
            Worksheets("Invoice").Range("E6").Value = ""
         MsgBox "Saved"
         Range("e7").Value = Range("e7").Value + 1
      Else
      MsgBox "No Data"
     
      End If
        
    
    
    End Sub
    
    
    Sub SaveAndPrint1()
    Worksheets("Invoice").Range("J9").Value = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
    AddInvoiceToList
    Worksheets("Invoice").Range("A1:F34").PrintOut From:=1, _
    To:=1, Copies:=2, Collate:=True
    Worksheets("Invoice").Range("B10:E24").Value = ""
    MsgBox "Saved"
    Range("e7").Value = Range("e7").Value + 1
    End Sub
    
    Sub Button1_Click()
    Worksheets("Invoice").Range("J9").Value = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
    ClearRow
    MsgBox "Saved"
    Worksheets("Invoice").Range("e7").Value = Worksheets("Invoice").Range("e7").Value + 1
    End Sub
    
    
    Sub ClearRow()
    Worksheets("Invoice").Range("B10:E24").Value = ""
     Worksheets("Invoice").Range("B6").Value = "Customer:  "
      Worksheets("Invoice").Range("E6").Value = ""
    End Sub
    
    Sub PrintInv()
    Worksheets("Invoice").Range("A1:F34").PrintOut From:=1, _
    To:=1, Copies:=2, Collate:=True
    
    End Sub
    
    Sub AddInvoiceToList()
    
      
      If Worksheets("Invoice").Range("I5").Value > 0 And Worksheets("Invoice").Range("B10").Value <> " " Or Worksheets("Invoice").Range("B10").Value <> "" Then
    Dim LR As Long, LRS As Long
    Dim x As Integer
    Dim ws As Worksheet
      Set ws = Worksheets("InvoiceList")
      LR = ws.Cells(Rows.Count, 1).End(xlUp).Row + 1
      LRS = ws.Cells(Rows.Count, 15).End(xlUp).Row + 1
      
        
        Dim tableName As ListObject, invoiceListTable As ListObject
        
        Set tableName = Worksheets("InvoiceList").ListObjects("InvoiceListTable")
        Set invoiceListTable = Worksheets("InvoiceList").ListObjects("InvoiceSummaryTable")
          
        Dim sumAddRow As ListRow
        Set sumAddRow = invoiceListTable.ListRows.Add()
       
        With sumAddRow
            .Range(1) = sumAddRow.Index
            .Range(2) = Worksheets("Invoice").Range("B28").Value
            .Range(3) = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
            .Range(4) = Replace(Worksheets("Invoice").Range("B6"), "Customer:", "")
            .Range(5) = Worksheets("Invoice").Range("E6").Value
            
            .Range(7) = Worksheets("Invoice").Range("i5").Value
            .Range(6) = Worksheets("Invoice").Range("C29").Value
            
        End With
        
        Dim addedRow As ListRow
        Set addedRow = tableName.ListRows.Add()
        
        
        With addedRow
            .Range(1) = addedRow.Index
            .Range(2) = Worksheets("Invoice").Range("B28").Value
            .Range(3) = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
            .Range(4) = Replace(Worksheets("Invoice").Range("B6"), "Customer:", "")
            .Range(5) = Worksheets("Invoice").Range("E6").Value
            .Range(6) = Worksheets("Invoice").Range("B10").Value
            .Range(7) = Worksheets("Invoice").Range("C10").Value
            .Range(8) = Worksheets("Invoice").Range("D10").Value
            .Range(9) = Worksheets("Invoice").Range("E10").Value
            .Range(10) = Worksheets("Invoice").Range("F10").Value
            .Range(11) = Worksheets("Invoice").Range("i5").Value
            .Range(12) = Worksheets("Invoice").Range("C29").Value
            
        End With
        
        'Add More Row to List
        Dim RowN As Long
        RowN = 11
         For x = 11 To 24
         
         If Worksheets("Invoice").Cells(x, 2).Value = " " Or Worksheets("Invoice").Cells(x, 2).Value = "" And Worksheets("Invoice").Cells(x, 4).Value = " " Or Worksheets("Invoice").Cells(x, 4).Value = "" Then
         RowN = x + 1
         
         Else
         
         Dim addedRow2 As ListRow
        Set addedRow2 = tableName.ListRows.Add()
        
        
        With addedRow2
            .Range(1) = addedRow2.Index
            .Range(2) = Worksheets("Invoice").Range("B28").Value
            .Range(3) = Worksheets("Invoice").Range("c7").Value & Worksheets("Invoice").Range("E7").Value
            .Range(4) = Replace(Worksheets("Invoice").Range("B6"), "Customer:", "")
            .Range(5) = Worksheets("Invoice").Range("E6").Value
            
            .Range(6) = Worksheets("Invoice").Cells(x, 2).Value
            .Range(7) = Worksheets("Invoice").Cells(x, 3).Value
            .Range(8) = Worksheets("Invoice").Cells(x, 4).Value
            .Range(9) = Worksheets("Invoice").Cells(x, 5).Value
            .Range(10) = Worksheets("Invoice").Cells(x, 6).Value
            
            .Range(11) = 0
            .Range(12) = " "
            
        End With
         End If
         
        Next x
      Else
      MsgBox "No Data"
      End If
        
    
    End Sub
    
    Sub ProfitLoss()
    
    Set StockRange = Worksheets("StockList").Range("A1:J7000")
    Dim x As Long
    x = Application.WorksheetFunction.VLookup(Worksheets("Invoice").Range("O1").Value, StockRange, 2, False)
   MsgBox x
    
    End Sub
