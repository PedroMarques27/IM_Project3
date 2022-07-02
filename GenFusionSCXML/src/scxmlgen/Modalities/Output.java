package scxmlgen.Modalities;

import scxmlgen.interfaces.IOutput;



public enum Output implements IOutput{
    

    SEAT("[action][SIT]"),
    CHAT("[action][SIT]"),
    PAUSE("[action][SIT]"),
    END("[action][SIT]"),
    START("[action][SIT]"),
    RAISE("[action][SIT]"),
    BET("[action][SIT]"),
    FOLD("[action][SIT]"),
    CHECK("[action][SIT]"),
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
