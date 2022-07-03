package scxmlgen.Modalities;

import scxmlgen.interfaces.IModality;

/**
 *
 * @author nunof
 */
public enum Gestures implements IModality{

    BOTH("[0][Both]",3500),
    RAISE("[3][Raise]",3500),
    BET("[4][Thumb]",3500),
    FOLD("[2][Fold]",3500),
    CHECK("[1][Check]",3500);



    private String event;
    private int timeout;


    Gestures(String m, int time) {
        event=m;
        timeout=time;
    }

    @Override
    public int getTimeOut() {
        return timeout;
    }

    @Override
    public String getEventName() {
        //return getModalityName()+"."+event;
        return event;
    }

    @Override
    public String getEvName() {
        return getModalityName().toLowerCase()+event.toLowerCase();
    }
    
}
