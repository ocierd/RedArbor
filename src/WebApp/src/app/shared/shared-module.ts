import { NgModule } from "@angular/core";
import { MaterialModule } from "./material/material-module";
import { FormField } from "@angular/forms/signals";
import { ComponentsModule } from "./components/components-module";


const sharedModules = [
    MaterialModule,
    ComponentsModule,


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
})
export class SharedModule {


}