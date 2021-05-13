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
To run this project, install it locally using :

```
$ cd ../lorem
$ npm install
$ npm start
```
