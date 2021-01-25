import {Component, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {Observable} from 'rxjs';
import { Position } from '../entities/position';
import { PositionService } from '../services/position.service';
   
@Component({ 
    selector: 'app-home', 
    templateUrl: './position.component.html',
    providers: [PositionService]
}) 
export class PositionComponent implements OnInit {
    @ViewChild('readOnlyTemplate', {static: false}) readOnlyTemplate: TemplateRef<any>;
    @ViewChild('editTemplate', {static: false}) editTemplate: TemplateRef<any>;
       
    editedPosition: Position;
    positions: Array<Position>;
    isNewRecord: boolean;
    statusMessage: string;
       
    constructor(private serv: PositionService) {
        this.positions = new Array<Position>();
    }
       
    ngOnInit() {
        this.loadPositions();
    }
       
    private loadPositions() {
        this.serv.getPositions().subscribe((data: Position[]) => {
                this.positions = data; 
            });
    }

    addPosition() {
        this.editedPosition = new Position(0,"");
        this.positions.push(this.editedPosition);
        this.isNewRecord = true;
    }
    
    editPosition(position: Position) {
        this.editedPosition = new Position(position.id, position.name);
    }

    loadTemplate(position: Position) {
        if (this.editedPosition && this.editedPosition.id === position.id) {
            return this.editTemplate;
        } else {
            return this.readOnlyTemplate;
        }
    }
    
    savePosition() {
        if (this.isNewRecord) {
            this.serv.createPosition(this.editedPosition).subscribe(data => {
                this.statusMessage = 'Data added successfully',
                this.loadPositions();
            });
            this.isNewRecord = false;
            this.editedPosition = null;
        } else {
            this.serv.updatePosition(this.editedPosition).subscribe(data => {
                this.statusMessage = 'Data update successfully',
                this.loadPositions();
            });
            this.editedPosition = null;
        }
    }

    cancel() {
        if (this.isNewRecord) {
            this.positions.pop();
            this.isNewRecord = false;
        }
        this.editedPosition = null;
    }
 
    deletePosition(position: Position) {
        this.serv.deletePosition(position.id).subscribe(data => {
            this.statusMessage = 'Данные успешно удалены',
            this.loadPositions();
        });
    }
}