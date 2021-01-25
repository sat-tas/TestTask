import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Position } from '../entities/position';

    
@Injectable({ providedIn: 'root' })
export class PositionService{
    
    private url = "https://localhost:44347/api/position";
    constructor(private http: HttpClient){ }
       
    getPositions(){
        return this.http.get(this.url+"/GetAllPositions");
    }
   
    createPosition(position: Position){
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.post(this.url+"/CreatePosition", JSON.stringify(position), {headers: myHeaders}); 
    }
 
    updatePosition(position: Position) {
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.put(this.url+"/UpdatePosition/"+position.id, JSON.stringify(position), {headers:myHeaders});
    }
 
    deletePosition(id: number){
        return this.http.delete(this.url + '/DeletePosition/' + id);
    }
}