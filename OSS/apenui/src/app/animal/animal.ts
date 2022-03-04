/*
export interface Animal {
    id: number;
    naam: string;
    soort: string;
}
*/

export class Animal implements Animal{
    id: number;
    naam: string;
    soort: string;

    constructor(id:number, naam:string, soort:string){
        this.id = id;
        this.naam = naam;
        this.soort = soort;
    }
}