package scxmlgen.Modalities;

import scxmlgen.interfaces.IModality;

/**
 *
 * @author nunof
 */
public enum Gestures implements IModality{

    BOTH_UP("[action][BOTH]",1500),
    RAISE("[action][RAISE]",1500),
    BET("[action][BET]",1500),
    FOLD("[action][FOLD]",1500),
    CHECK("[action][CHECK]",1500)
    ;



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
