function  event_say(choice)
diagOptions = {}
    npcDialogue = "We protect Warlord Jurglash with our lives, night and day. Where he go, we go. And our axes go."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end