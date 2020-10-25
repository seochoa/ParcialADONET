import { Component, OnInit } from '@angular/core';
import { Apoyo } from '../models/apoyo';
import { ApoyoService } from '../services/apoyo.service';
import { Persona } from '../models/persona';

@Component({
  selector: 'app-consulta',
  templateUrl: './consulta.component.html',
  styleUrls: ['./consulta.component.css']
})
export class ConsultaComponent implements OnInit {

  Apoyos : Apoyo[] = [];
  Personas : Persona[];
  constructor(private apoyoService: ApoyoService) { }

  ngOnInit(): void {
    this.get();
   
  }
  get(){
    this.apoyoService.get().subscribe(result => {
      this.Apoyos = result;
      });
    
  }

  

}
