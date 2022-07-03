/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package genfusionscxml;

import java.io.IOException;
import scxmlgen.Fusion.FusionGenerator;
import scxmlgen.Modalities.Gestures;
import scxmlgen.Modalities.Output;
import scxmlgen.Modalities.Speech;

/**
 *
 * @author nunof
 */
public class GenFusionSCXML {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws IOException {

    FusionGenerator fg = new FusionGenerator();


    fg.Complementary(Gestures.BOTH_UP, Speech.PLAYERS, Output.PLAYERS);
    fg.Complementary(Gestures.BOTH_UP, Speech.OPTIONS, Output.OPTIONS);
    fg.Complementary(Gestures.RAISE, Speech.BETVALUE, Output.RAISE_VALUE);

    fg.Redundancy(Speech.RAISE, Gestures.RAISE, Output.RAISE);
    fg.Redundancy(Speech.BET, Gestures.BET, Output.BET);
    fg.Redundancy(Speech.FOLD, Gestures.FOLD, Output.FOLD);
    fg.Redundancy(Speech.CHECK, Gestures.CHECK, Output.CHECK);



    //fg.Single(Speech.CIRCLE, Output.CIRCLE);  //EXAMPLE
    //fg.Redundancy(Speech.CIRCLE, SecondMod.CIRCLE, Output.CIRCLE);  //EXAMPLE
    
    fg.Build("fusion.scxml");
        
        
    }
    
}
