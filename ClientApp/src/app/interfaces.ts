import { ModuleWithProviders } from '@angular/core';

export interface IClient {
  id?: number;
  FirstName: string;
  LastName: string;
  Email: string;
  Address: string;
  City: string;
  Gender: string;
  CommandeCount: number;
  Commandes?: ICammande[];
  
}
export interface ICammande {
  id?: number;
  Quantity: number;
  Price: number;
  Articles: IProduct[];
    
}

export interface IProduct {
  id?: number;
  productCategory: string;
  productName: string;
  productDescription: string;
  productPrice: number;
  productWeight: number;
}



