package scxmlgen.Modalities;

import scxmlgen.interfaces.IOutput;



public enum Output implements IOutput{
    

    SEAT("[action][SIT]"),
    CHAT("[action][CHAT]"),
    PAUSE("[action][PAUSE]"),
    END("[action][END]"),
    START("[action][START]"),
    RAISE("[action][RAISE]"),
    BET("[action][BET]"),
    FOLD("[action][FOLD]"),
    CHECK("[action][CHECK]"),
    PLAYERS("[action][BOTH][type][PLAYERS]"),
    OPTIONS("[action][BOTH][type][OPTIONS]"),
    RAISE_VALUE("[action][RAISE][value][10]")
    ;

    
    private String event;

    Output(String m) {
        event=m;
    }
    
    public String getEvent(){
        return this.toString();
    }

    public String getEventName(){
        return event;
    }
}
