import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Pagination } from '../shared/models/Pagination';
import { Product } from '../shared/models/Product';
import { Brand } from '../shared/models/Brand';
import { Type } from '../shared/models/Type';
import { ShopParams } from '../shared/models/ShopParams';


@Injectable({
  providedIn: 'root'
})
export class ShopService {
 baseUrl = 'https://localhost:5005/api/';
  constructor(private http : HttpClient) { }
  getProducts(shopParams :ShopParams){
    let params = new HttpParams();
    if(shopParams.brandId) params = params.append('brandId',shopParams.brandId);
    if(shopParams.typeId) params = params.append('typeId',shopParams.typeId);
    //if(sort) params = params.append('sort',sort);
    params = params.append('sort',shopParams.sort);
    params = params.append('pageIndex',shopParams.pageNumber);
    params =  params.append('pageSie',shopParams.pageSize);
    if(shopParams.search)
    params = params.append('search',shopParams.search);
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products',{params});
  }
  getBrands(){
  return this.http.get<Brand[]>(this.baseUrl +'products/brands');
  }
  getTypes(){
    return this.http.get<Type[]>(this.baseUrl +'products/types');
  }
  getProduct(id:number){
    return this.http.get<Product>(this.baseUrl +'products/'+id);
  }
}
