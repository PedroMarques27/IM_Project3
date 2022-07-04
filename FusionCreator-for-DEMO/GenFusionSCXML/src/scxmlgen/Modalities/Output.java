package scxmlgen.Modalities;

import scxmlgen.interfaces.IOutput;



public enum Output implements IOutput{

    RAISE("[action][RAISE]"),
    BET("[action][BET]"),
    FOLD("[action][FOLD]"),
    CHECK("[action][CHECK]"),
    PLAYERS("[action][BOTH][type][PLAYERS]"),
    OPTIONS("[action][BOTH][type][OPTIONS]"),
    RAISE_VALUE_MIN("[action][RAISE][value][MIN]"),
    RAISE_VALUE_DUP("[action][RAISE][value][DUPL]"),
    RAISE_VALUE_ALL("[action][RAISE][value][ALL]"),
    CHAT("[action][CHAT]"),
    PAUSE("[action][PAUSE]"),
    END("[action][END]"),
    START("[action][START]"),
    RESUME("[action][RESUME]"),
    CHATON("[action][CHATON]"),
    CHATOFF("[action][CHATOFF]"),
    MIN("[action][MIN]"),
    DUPL("[action][DUPL]"),
    ALL("[action][ALL]");
    
    
    
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
