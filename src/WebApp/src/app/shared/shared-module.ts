import { NgModule } from "@angular/core";
import { MaterialModule } from "./material/material-module";
import { FormField } from "@angular/forms/signals";
import { ComponentsModule } from "./components/components-module";
import { PipesModule } from "./pipes/pipes-module";
import { DatePipe } from "@angular/common";


const sharedModules = [
    MaterialModule,
    ComponentsModule,
    PipesModule,

    FormField
];


@NgModule({
    declarations: [
        
  ],
    imports: [

        ...sharedModules
    ],
    exports: [
        ...sharedModules
    ],

    providers:[
        DatePipe
    ]
})
export class SharedModule {


}