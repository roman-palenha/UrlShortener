import { Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Subject } from 'rxjs/internal/Subject';
import { takeUntil } from 'rxjs';

export interface ShortenUrl{
    id: number,
    createdBy : string,
    createdOn : Date,
    fullUrl : string,
    shorten : string
}

export class UrlViewModel {
  public url! : string
  public createdBy! : string
  constructor(url : string){
    this.url = url;
    this.createdBy = "";
  }
}

@Component({
  selector: 'app-url-table',
  templateUrl: './url-table.component.html',
  styleUrls: ['./url-table.component.css']
})
@Injectable()
export class UrlTableComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['Original URL', 'Shorten URL', 'Options'];
  dataSource!: ShortenUrl[];
  inputUrl! : string;
  componentDestroyed$: Subject<boolean> = new Subject();

  constructor(private http : HttpClient){
  }

  ngOnInit(){
    this.http.get<ShortenUrl[]>("https://localhost:7164/Url/GetAll").pipe(takeUntil(this.componentDestroyed$)).subscribe(result => this.dataSource = result);
  }

  ngOnDestroy(){
    this.componentDestroyed$.next(true);
    this.componentDestroyed$.complete();
  }

  onAdd(){
    if(this.dataSource.find(x => x.fullUrl === this.inputUrl) != null)
      return;
    
    this.http.post<ShortenUrl>("https://localhost:7164/Url/Add", new UrlViewModel(this.inputUrl)).pipe(takeUntil(this.componentDestroyed$)).subscribe(result => this.dataSource.push(result));
  }

  onDelete(url : ShortenUrl){
    this.http.delete(`https://localhost:7164/Url/Remove?id=${url.id}`).subscribe(result => this.dataSource = this.dataSource.filter(x => x.id != url.id));
  }

  onInfo(url : ShortenUrl){
    window.open(`https://localhost:7164/Url/Info?id=${url.id}`);
  }
}