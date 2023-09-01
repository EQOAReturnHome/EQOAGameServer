function  event_say(choice)
diagOptions = {}
    npcDialogue = "Why would I accept this post? Freeport is the trade capital of Tunaria. *Anything* could happen here. Anything. That excites me, doesn't it excite you playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end