# MediaValet-Using-Azure-Storage-
## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)

## General info
This project is simple a Ordering system that allows users to place and order, Which is saved to an azure queue and the Processing application picks the orders from the queue and processes them.
After which the processed orders are saved to an Azure table, to be retrieved by the Supervisor API
	
## Technologies
Project is created with:
* .Net 4.7.2
* Azure Storage Emulator
* Azure Storage Services: Queues, Table, Blob
	
## Setup
To run this project, Open it on Visual studio And run the Supervisor API Project. Then Create new Orders using the Post Order Url:"https://localhost:portnumber/api/order", using post man or any other software. After posting as many orders as you want. Then run the "Orderconsole" Application. Which will pick the posted orders from the queue and process them. When its done processing the order. The Message indicating it is done, will be displayed on the consele. 
To see all processed order, call the Get orders URL: "https://localhost:portnumber/api/order" on post man, and all processed orders will be returned.  It was not stated in the specification file, Console application would be a web job, So I did not make it one.

Successfully posted orders will return httpstatus code 200.




```

```
