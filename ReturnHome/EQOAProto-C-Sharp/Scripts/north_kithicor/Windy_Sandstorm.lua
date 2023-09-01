function event_say()
diagOptions = {}
    npcDialogue = "I do love it here in Kith, but...It does get a bit lonely. Thank you. It's just nice to talk to someone."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end