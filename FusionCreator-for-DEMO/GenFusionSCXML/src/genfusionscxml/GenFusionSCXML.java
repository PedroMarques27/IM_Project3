/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package genfusionscxml;

import java.io.IOException;
import scxmlgen.Fusion.FusionGenerator;
import scxmlgen.Modalities.Output;
import scxmlgen.Modalities.Speech;
import scxmlgen.Modalities.SecondMod;

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

    fg.Single(Speech.CHAT, Output.CHAT);
    fg.Single(Speech.PAUSE, Output.PAUSE);
    fg.Single(Speech.END, Output.END);
    fg.Single(Speech.RESUME, Output.RESUME);
    fg.Single(Speech.START, Output.START);
    fg.Single(Speech.FOLD, Output.FOLD);
    fg.Single(Speech.BET, Output.BET);
    fg.Single(Speech.RAISE, Output.RAISE);
    fg.Single(Speech.CHECK, Output.CHECK);



    fg.Single(SecondMod.BOTH, Output.PLAYERS);
    fg.Single(SecondMod.BET, Output.BET);
    fg.Single(SecondMod.FOLD, Output.FOLD);
    fg.Single(SecondMod.RAISE, Output.RAISE);
    fg.Single(SecondMod.CHECK, Output.CHAT);

    fg.Complementary(SecondMod.BOTH, Speech.PLAYERS, Output.PLAYERS);
    fg.Complementary(SecondMod.BOTH, Speech.OPTIONS, Output.OPTIONS);
    fg.Complementary(SecondMod.RAISE, Speech.ALL_IN, Output.RAISE_VALUE_ALL);
    fg.Complementary(SecondMod.RAISE, Speech.MIN, Output.RAISE_VALUE_MIN);
    fg.Complementary(SecondMod.RAISE, Speech.DUPLICATE, Output.RAISE_VALUE_DUP);

    
    //fg.Single(Speech.CIRCLE, Output.CIRCLE);  //EXAMPLE
    //fg.Redundancy(Speech.CIRCLE, SecondMod.CIRCLE, Output.CIRCLE);  //EXAMPLE
    
    fg.Build("fusion.scxml");
        
        
    }
    
}
