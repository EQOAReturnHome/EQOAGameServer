function  event_say(choice)
diagOptions = {}
    npcDialogue = "Hmm, it says, \"Meet at the well near the docks if you want your...\"  OH EXCUSE ME. Sorry, I need to be alone right now."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end