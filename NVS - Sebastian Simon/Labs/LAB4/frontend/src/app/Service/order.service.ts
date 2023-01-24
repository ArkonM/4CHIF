import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {OrderRequestModel} from "../model/oderRequest.model";
import {ApiResponse} from "../model/api.response";

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http:HttpClient) { }

  public addOrder(order: OrderRequestModel):Observable<ApiResponse>{
    return this.http.put<ApiResponse>('http://localhost:8081/orders/add', order);
  }

}
