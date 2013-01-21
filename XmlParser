#Graph.rb

class Graph
  class Node
    def getName
      return @name
    end
    def getValue
      return @value
    end
    
    def initialize(_name="",_value=0)
      @name=_name
      @value=_value
      @children=[]
      @parentNode=self
    end
    
    def assignParent(parentNode)
      @parentNode=parentNode
    end
    def getParent()
      return @parentNode
    end
    
    def addNode(node)
      node.assignParent(self)
      @children.push(node)
    end
    
    def getChildNodes
      return @children
    end
    
    def getDepth(rootName="Root")
      node=self
      level=0
      
      while(node.getParent().getName!=rootName)
        node=node.getParent
        level+=1
      end
      return level
    end
  end
  
  def printGraph(node)
    puts "Node: "+node.getName+" Parent: "+node.getParent.getName
    for n in node.getChildNodes
      printGraph(n)
    end
  end
  
  def printUsingLevels(node)
    level=node.getDepth()
    i=0
    print "|"
    while(i<level)
      print "-"
      i+=1
    end
    print("->")
    puts node.getName
    #puts "Node: "+node.getName+" Parent: "+node.getParent.getName
    for n in node.getChildNodes
      printUsingLevels(n)
    end
  end
  def searchAndGetNode(node,name)
    @foundNode=nil
    children=node.getChildNodes

    if(node.getName==name)
      puts "Found in node with value "+node.getValue.to_s
      @foundNode=node
    else
      #puts "Searching=> Current Node: "+ node.getName + " Required: "+name
      for child in children
        searchAndGetNode(child,name)
      end
    end
    return @foundNode
  end

  #Check the parent existance and get a reference to the parent
  def addNode(parentName,node)
    parent=searchAndGetNode(@root,parentName)
    if(parent==nil)
      raise "Trying to add a node to a non existing node name"+parentName.to_s
      return
    end
    addToTree(parent,node)#Add to the tree
  end
  
  def addToTree(parentNode,node)
    if(node!=nil)
      parentNode.addNode(node)
    end
  end
  
  def initialize(rootNode)
    @root=rootNode

  end
  
end

=begin

root=Graph::Node.new("Root",100)
g=Graph.new(root)
g.addNode("Root",Graph::Node.new("RootChild",90))
g.addNode("Root",Graph::Node.new("RootChild1",110))
g.addNode("Root",Graph::Node.new("RootChild2",200))
g.addNode("Root",Graph::Node.new("RootChild3",300))

childNode=g.searchAndGetNode(root,"RootChild3")


if( childNode.instance_of? Graph::Node and childNode!=nil)
  puts childNode.getParent.getName
else
  puts "Couldn't find"
end

childNode.addNode(Graph::Node.new("RootChild's Child",500))
childNode=g.searchAndGetNode(root,"RootChild's Child")
if( childNode.instance_of? Graph::Node and childNode!=nil)
  puts childNode.getParent.getName
else
    puts "Couldn't find"
end

puts "Graph Produced:"
g.printGraph(root)
puts "LEVEL Based Print"
g.printUsingLevels(root)

=end

################################################

#Xml Parser.rb

class XmlEntry
    def initialize(text,openTag=false,closeTag=false)
      
      @text=text
      @openTag=openTag
      @closeTag=closeTag
      @hasChildren=false
      @children=[]
    end
    
    def hasChildren()
      return @hasChildren
    end
    
    def addChildren(list)
      @children=list
      @hasChildren=true
    end
    
    def getChildren()
      return @children
    end
    
    def getText()
      return @text
    end
    
    def getCloseTag()
      return @closeTag
    end
    
    def getOpenTag()
      return @openTag
    end
    
 end
  
  
class XMLParser
 
  def initialize()
    @dictionary={}
    @parsedResult=[]
    @root=Graph::Node.new("Root",0)
    @graph=g=Graph.new(@root)
    
  end

  #CURRENTLY JUST DOES FOR 1 LINE <<<<<<<<<<<<<<<<<<<<< 1 Global string 
  def readXML(fName)
    puts Dir.pwd
    file=File.open(fName,"r")
    while !file.eof?
      @line=file.readline
      puts @line
    end
  end

  def parseXML()
    @currentParent="Root"
    @openedTagTracker=[]
    @elementTracker=[] # The first element in the list will be like a grand parent of all?
    line=@line
    i=0
    while(i<line.size)
      openTag=true
      tagElement=""
      #Case 1: </ Data >      A  closing tag
      if(line[i]=="<" and line[i+1]=="/")
        while(line[i]!=">") #find end tag
          tagElement+=line[i]
          i+=1
        end
        #CLOSE TAG
        if(@openedTagTracker!=nil)
          elementToClose=@openedTagTracker.last
          @openedTagTracker.pop
          @currentParent=@openedTagTracker.last
          puts "Iterating back , Parent is "+@currentParent.to_s
          @parsedResult.push(XmlEntry.new(elementToClose,false,true))#<------- 
          puts "CLOSING TAG:"+elementToClose
        end

        #puts "END TAG: "+tagElement
        #Case 2: < Data >     An open tag
      elsif(line[i]=="<") #find start tag
        i+=1
        while(line[i]!=">") #find end tag
          #Case 3: < Data />   A single Element
          if(line[i]=="/")
            openTag=false
          else
            tagElement+=line[i]
          end
          i+=1
        end
        if(openTag)
          attribs=getAttributes(tagElement)
          @openedTagTracker.push(@elementName) #getAttributes method updates the element name
          puts "Current Parent: "+@currentParent
          #@parsedResult.push(XmlEntry.new(elementToClose,true,false))#<-------
          puts "OPEN TAG "+@elementName
          puts @currentParent + " , Child: "+ @elementName
          @graph.addNode(@currentParent,Graph::Node.new(@elementName,attribs))
          @currentParent=@openedTagTracker.last
          next
        end
        
        attribs=getAttributes(tagElement)
        puts @currentParent + " , Child: "+ @elementName
        @graph.addNode(@currentParent,Graph::Node.new(@elementName,attribs))
          
      end
      i+=1
    end
    #puts "Parent Dir"
    #puts @elementTracker
  end

  #Given an xml line without tags, this method returns a
  #hashmap/dictionary which has element => attributes mappings
  #The attributes will be inserted as a list
  def getAttributes(xmlString)
    quotesRemoved=xmlString.gsub("\"","")

    splitted=quotesRemoved.split(" ") #Split by spaces
    
    @elementName=splitted[0]
    splitted.delete(@elementName)
    if(@elementName.include? "</element")
      return []
    end
    @dictionary[@elementName]=splitted
    @elementTracker.push(@elementName)
    #Now split by equal and get in a  mapped format
    
    #puts splitted
    return splitted
  end
  def getParsedResult()
    return @parsedResult
  end
  
  def printGraph()
    @graph.printUsingLevels(@root)
  end

end

parser=XMLParser.new
parser.readXML("Sample.xml")
parser.parseXML
puts "   \n\n\n\n\n\n ::::::: PRINT GRAPH (Level Based) :::::::: \n"
#parser.printGraph

#Schema Parser
##################################################################
require_relative "Graph"

class XmlEntry
    def initialize(text,openTag=false,closeTag=false)
      
      @text=text
      @openTag=openTag
      @closeTag=closeTag
      @hasChildren=false
      @children=[]
    end
    
    def hasChildren()
      return @hasChildren
    end
    
    def addChildren(list)
      @children=list
      @hasChildren=true
    end
    
    def getChildren()
      return @children
    end
    
    def getText()
      return @text
    end
    
    def getCloseTag()
      return @closeTag
    end
    
    def getOpenTag()
      return @openTag
    end
    
 end
  
  
class XMLParser
 
  def initialize()
    @dictionary={}
    @parsedResult=[]
    @root=Graph::Node.new("Root",0)
    @graph=g=Graph.new(@root)
    
  end

  #CURRENTLY JUST DOES FOR 1 LINE <<<<<<<<<<<<<<<<<<<<< 1 Global string 
  def readXML(fName)
    puts Dir.pwd
    file=File.open(fName,"r")
    while !file.eof?
      @line=file.readline
      puts @line
    end
  end

  def parseXML()
    @currentParent="Root"
    @openedTagTracker=[]
    @elementTracker=[] # The first element in the list will be like a grand parent of all?
    line=@line
    i=0
    while(i<line.size)
      openTag=true
      tagElement=""
      #Case 1: </ Data >      A  closing tag
      if(line[i]=="<" and line[i+1]=="/")
        while(line[i]!=">") #find end tag
          tagElement+=line[i]
          i+=1
        end
        #CLOSE TAG
        if(@openedTagTracker!=nil)
          elementToClose=@openedTagTracker.last
          @openedTagTracker.pop
          @currentParent=@openedTagTracker.last
          puts "Iterating back , Parent is "+@currentParent.to_s
          @parsedResult.push(XmlEntry.new(elementToClose,false,true))#<------- 
          puts "CLOSING TAG:"+elementToClose
        end

        #puts "END TAG: "+tagElement
        #Case 2: < Data >     An open tag
      elsif(line[i]=="<") #find start tag
        i+=1
        while(line[i]!=">") #find end tag
          #Case 3: < Data />   A single Element
          if(line[i]=="/")
            openTag=false
          else
            tagElement+=line[i]
          end
          i+=1
        end
        if(openTag)
          attribs=getAttributes(tagElement)
          @openedTagTracker.push(@elementName) #getAttributes method updates the element name
          puts "Current Parent: "+@currentParent
          #@parsedResult.push(XmlEntry.new(elementToClose,true,false))#<-------
          puts "OPEN TAG "+@elementName
          puts @currentParent + " , Child: "+ @elementName
          @graph.addNode(@currentParent,Graph::Node.new(@elementName,attribs))
          @currentParent=@openedTagTracker.last
          next
        end
        
        attribs=getAttributes(tagElement)
        puts @currentParent + " , Child: "+ @elementName
        @graph.addNode(@currentParent,Graph::Node.new(@elementName,attribs))
          
      end
      i+=1
    end
    #puts "Parent Dir"
    #puts @elementTracker
  end

  #Given an xml line without tags, this method returns a
  #hashmap/dictionary which has element => attributes mappings
  #The attributes will be inserted as a list
  def getAttributes(xmlString)
    quotesRemoved=xmlString.gsub("\"","")
    attribNameRemoved=quotesRemoved.gsub("attribute name=","")
    elementNameRemoved=attribNameRemoved.gsub("element name=","")

    elementNameRemoved=elementNameRemoved.gsub("structureElement name=","")
    splitted=elementNameRemoved.split(" ") #Split by spaces
    
    @elementName=splitted[0]
    splitted.delete(@elementName)
    if(@elementName.include? "</element")
      return []
    end
    @dictionary[@elementName]=splitted
    @elementTracker.push(@elementName)
    #Now split by equal and get in a  mapped format
    
    #puts splitted
    return splitted
  end
  def getParsedResult()
    return @parsedResult
  end
  
  def printGraph()
    @graph.printUsingLevels(@root)
  end

end

parser=XMLParser.new
parser.readXML("Schema.xml")
parser.parseXML
puts "   \n\n\n\n\n\n ::::::: PRINT GRAPH (Level Based) :::::::: \n"
parser.printGraph
