import { Inject, Injectable } from '@angular/core';
import { BaseService } from '../bases/base-service';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RedarborBaseService extends BaseService {
  constructor(@Inject('') controller: string) {
    super(environment.apiUrl+`/${controller}`);
  }
  



}
