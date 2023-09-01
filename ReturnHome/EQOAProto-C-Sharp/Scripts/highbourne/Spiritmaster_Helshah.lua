function event_say()
diagOptions = {}
    npcDialogue = "I can bind your spirit to this place, if you so wish. Be warned that your body and equipment will return here upon your demise, but with a terrible debt."
SendDialogue(mySession, npcDialogue, diagOptions)
end