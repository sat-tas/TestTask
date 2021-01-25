import { isConstructorDeclaration } from "typescript"

export class RequestParam
{
    constructor(
        public TotalCount:number,
        public PageSize:number,
        public CurrentPage:number
    )
    {
        
    }
}