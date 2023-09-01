function  event_say(choice)
diagOptions = {}
    npcDialogue = "If you think I look rough, you should see the goblins that attacked the wall. They haven't seen the last of me, thanks to Pora and her clerics."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end