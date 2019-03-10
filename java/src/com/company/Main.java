package com.company;

import java.io.*;
import java.nio.file.Files;
import java.util.Properties;

public class Main {

    public static void main(String[] args) throws IOException {
	// write your code here
        Properties properties=getConfig();

        File file=new File(properties.getProperty("filePath"));
        InputStreamReader isr=new InputStreamReader(new FileInputStream(file));
        BufferedReader bf=new BufferedReader(isr);
        String path="";
        while((path=bf.readLine())!=null){
            copyHandle(path,properties);
        }
        bf.close();
        isr.close();
    }

    private static void copyHandle(String path,Properties properties){
        File sourceFile = new File(properties.getProperty("sourceFilePath")+path);
        if(!sourceFile.exists()){
            System.out.println("源文件："+properties.getProperty("sourceFilePath")+path+"不存在！");
            return;
        }
        File targetFile = new File(properties.getProperty("targetFilePath")+path);
        //目标目录不存在则新增
        if(!targetFile.getParentFile().exists()){
            targetFile.getParentFile().mkdirs();
        }
        //目标文件存在则先删除
        if(targetFile.exists()){
            targetFile.delete();
        }
        try {
            Files.copy(sourceFile.toPath(),targetFile.toPath());
            System.out.println(sourceFile.toPath()+"复制成功");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static Properties getConfig(){
        Properties properties=new Properties();
        try {
            BufferedReader br=new BufferedReader(new FileReader("config"));
            properties.load(br);
        } catch (IOException e) {
            e.printStackTrace();
            properties=null;
        }
        return properties;
    }
}
