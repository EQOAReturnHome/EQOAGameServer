function event_say()
diagOptions = {}
    npcDialogue = "I once dueled the stone golem in the next room. It is a slow but formidable foe. It's attacks may be easily dodged, but one small mistake and you will be knocked out for a day."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end