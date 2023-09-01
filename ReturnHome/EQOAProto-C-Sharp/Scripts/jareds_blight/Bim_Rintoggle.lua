function  event_say(choice)
diagOptions = {}
    npcDialogue = "When I lived in in Qeynos I read about Jared and this tower. I was so intrigued that I made the long journey here. Now that I am here I don't think I can ever leave. There is something alluring. Like I am being called to it. I can't explain it."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end