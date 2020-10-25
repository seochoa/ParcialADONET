import { Component, OnInit } from '@angular/core';
import { Apoyo } from '../models/apoyo';
import { ApoyoService } from '../services/apoyo.service';
import { Persona } from '../models/persona';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {

  Persona: Persona;
  Apoyo:Apoyo;

  constructor(private apoyoService: ApoyoService) { }

  ngOnInit(): void {
    this.Apoyo = new Apoyo();
    this.Persona = new Persona();
  }


  agregar(){
    this.Apoyo.persona = this.Persona;
    this.apoyoService.post(this.Apoyo).subscribe(p=>{
      if (p != null){
        alert('Apoyo Registrado')
        this.Apoyo = p;
      }
    });
  }

}
