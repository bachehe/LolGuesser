import { Component, OnInit } from '@angular/core';
import { MergedCharacter } from '../shared/models/mergedCharacter';
import { WarItemService } from './war-item.service';
import { catchError, map, retry, switchMap, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';

@Component({
  selector: 'app-war-item',
  templateUrl: './war-item.component.html',
  styleUrls: ['./war-item.component.scss']
})

export class WarItemComponent implements OnInit {
  champions$: Observable<{ item: string[], itemPictureUrl: string[], character: MergedCharacter[] }> | undefined;
  warChampions: MergedCharacter[] = [];

  constructor(private warService: WarItemService){
  }

  ngOnInit(): void {
    this.champions$ = this.warService.getChampions();
    this.champions$.subscribe(data => console.log(data));
    this.champions$.forEach(x => console.log(x.character));
  }
}
