import {ProductValue} from "./productValue.model";


export class OrderRequestModel{
  anrede!: string;
  vorname!: string;
  name!: string;
  strasse!: string;
  plz!: string;
  ort!: string;

  product: Array<ProductValue> = new Array<ProductValue>();
}
