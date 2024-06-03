import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from 'src/app/shared/models/Product';
import { ShopService } from '../shop.service';
import { Brand } from 'src/app/shared/models/Brand';
import { Type } from 'src/app/shared/models/Type';
import { ShopParams } from 'src/app/shared/models/ShopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm? :ElementRef;
products : Product[]=[];
brands:Brand[] =[];
types:Type[] =[];
totalCount =0;
// brandIdSelected = 0;
// typeIdSelected = 0;
// sortSelected = 'name';
shopParams = new ShopParams();
sortOptions=[
  {name:"Alphabetical",value:"name"},
  {name:'Price:Low to High',value:"priceAsc"},
  {name:'Price:High to Low',value:"priceDesc"}
]
constructor(private ShopService : ShopService){}
ngOnInit(): void {
this.getBrands();
this.getProducts();
this.getTypes();
}
getProducts(){
  debugger;
  this.ShopService.getProducts(this.shopParams).subscribe({
    next : response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    },
    error :error =>  console.log(error)
    
  })
}
getBrands(){
  this.ShopService.getBrands().subscribe({
   // next : response => this.brands = response,
    next : response => this.brands = [{id:0,name:"All"},...response],
    error :error =>  console.log(error)
    
  })
}
getTypes(){
  this.ShopService.getTypes().subscribe({
    next : response => this.types = [{id:0,name:"All"},...response],
    error :error =>  console.log(error)
    
  })
}
onBrandSelected(brandId:number){
  debugger
  this.shopParams.brandId = brandId;
  this.shopParams.pageNumber = 1;
  this.getProducts();
}
onTypeSelected(typeId:number){
  debugger
  this.shopParams.typeId = typeId;
  this.shopParams.pageNumber = 1;
  this.getProducts();
}
onSortSelected(event:any){
  this.shopParams.sort = event.target.value;
  this.getProducts();
}
onPageChanged(event:any){
  if(this.shopParams.pageNumber !== event){
    this.shopParams.pageNumber = event;
    this.getProducts();
  }
}
onSearch(){
  this.shopParams.search = this.searchTerm?.nativeElement.value;
  this.shopParams.pageNumber = 1;
  this.getProducts();
}
onReset(){
  if(this.searchTerm) this.searchTerm.nativeElement.value = '';
  this.shopParams = new ShopParams();
  this.getProducts()
}
}