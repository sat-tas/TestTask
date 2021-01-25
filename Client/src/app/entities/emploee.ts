export class Employee{
    constructor(
        public id: number,
        public name: string,
        public surname: string,
        public patronymic: string,
        public departmentId:number,
        public departmentName:string,
        public positionId:number,
        public positionName:string,
        public dateAdded : Date,
        public dateChangeInfo : Date,
        public dateOfHiring : Date
        ) {
            
         }
}
